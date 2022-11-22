import { Component } from "react"
import { AiFillDelete } from "react-icons/ai";
import { FaEdit, FaToggleOff, FaToggleOn } from "react-icons/fa";
import { toast } from "react-toastify";
import { API_END_POINTS } from "../../Components/Core/Constants/EndPoints";
import { IResult } from "../../Components/Core/Dto/IResultObject";
import { IPagingResult } from "../../Components/Core/Dto/Paging";
import { IUserDto } from "../../Components/Core/Dto/Users";
import { Gender, UserRoles } from "../../Components/Enums";
import { Service } from "../../Components/Service";
import { Utility } from "../../Components/Service/Utility";
import { Loader } from "../Components/Loader";
import PageTitle from "../Components/Navigation/PageTitle";
import { Paging } from "../Components/Paging";
import { ToastError } from "../Components/ToastError";

class SearchParams {
    sortOrder: string = "ASC"
    sortBy: string = "createdOn"
    skip: number = 0
    take: number = 20
    searchText: string = ''
    userRole?: number
    includeDeleted: boolean = false
}
export default class Users extends Component<{}, { isUpdating: boolean, paging: IPagingResult, search: SearchParams, users: IUserDto[], isLoading: boolean, user: IUserDto }>{

    constructor(props: {}) {
        super(props)

        this.state = {
            isUpdating: false,
            users: [],
            isLoading: true,
            paging: {
                nextDisabled: true,
                prevDisabled: true,
                text: {
                    from: 0,
                    to: 0,
                    count: 0
                },
                prevPage: this.prevPage,
                nextPage: this.nextPage
            },
            search: new SearchParams(),
            user: {
                emailAddress: '',
                firstName: '',
                isActive: false,
                lastName: '',
                uniqueId: '',
                imagePath: '',
                role: 0,
                gender: 0
            }
        }
    }

    async componentDidMount() {

        this.getUsers(0);
    }

    getUsers = async (skip: number) => {
        var s = this.state.search;
        var q = `?skip=${skip}&take=${s.take}&sortBy=${s.sortBy}&sortOrder=${s.sortOrder}`;
        if (Utility.Validate.String(s.searchText))
            q += `&searchText=${s.searchText}`;
        if (s.includeDeleted)
            q += `&includeDeleted=${s.includeDeleted}`;

        var response = await Service.Get<IResult<IUserDto[]>>(`${API_END_POINTS.USERS}${q}`);

        if (response.hasErrors) {
            toast.error(<ToastError errors={response.errors} />)
            this.setState({ isLoading: false });
            return;
        }
        if (response.resultObject) {
            var paging = Service.ApplyPaging(this.state.search.skip, response.rowCount, this.state.search.take);
            this.setState({
                isLoading: false, users: response.resultObject,
                paging: {
                    ...this.state.paging,
                    text: paging.text, nextDisabled: paging.nextDisabled,
                    prevDisabled: paging.prevDisabled
                }
            });

        }
        else {
            toast.info('Currently there is no data')
            this.setState({ isLoading: false });
        }
    }

    nextPage = async () => {
        var skip = this.state.search.skip + this.state.search.take;
        this.setState({ isLoading: true, search: { ...this.state.search, skip: skip } });
        this.getUsers(skip);
    }

    prevPage = async () => {
        var skip = this.state.search.skip - this.state.search.take;
        this.setState({ isLoading: true, search: { ...this.state.search, skip: skip } });
        this.getUsers(skip);
    }

    createUser = async (e: React.MouseEvent<HTMLButtonElement>) => {
        const { firstName, lastName, emailAddress, gender, role } = this.state.user;
        e.preventDefault();
        var errors = [];
        if (!Utility.Validate.String(firstName)) {
            errors.push('Please enter first name');
        }
        if (!Utility.Validate.String(lastName)) {
            errors.push('Please enter last name');
        }
        if (!Utility.Validate.String(emailAddress)) {
            errors.push('Please enter email address');
        }
        if (!Utility.Validate.Number(gender)) {
            errors.push('Please select gender');
        }
        if (!Utility.Validate.Number(role)) {
            errors.push('Please select role');
        }
        if (errors.length > 0) {
            toast.error(<ToastError errors={errors} />);
            return;
        }
        this.setState({ isLoading: true });
        var formData = {
            firstName: firstName,
            lastName: lastName,
            emailAddress: emailAddress
        }
        var response = await Service.Post<IResult<IUserDto>>(API_END_POINTS.USERS, formData);

        if (response.hasErrors) {
            toast.error(<ToastError errors={response.errors} />)
            this.setState({
                isLoading: false
            })
            return;
        }
        toast.success(`User account created succssfully`);
        this.setState({
            isLoading: false,
            user: {
                ...this.state.user,
                firstName: '',
                lastLoginDate: undefined,
                lastName: '',
                emailAddress: ''
            }
        })
        this.getUsers(0);
    }

    deleteUser = async (user: IUserDto) => {
        if (window.confirm(`Are you sure to delete ${user.firstName} ?`)) {


            this.setState({ isLoading: true })
            var updateResponse = await Service.Delete<IResult<boolean>>(`${API_END_POINTS.USERS}/${user.uniqueId}`, {});

            this.setState({ isLoading: false })
            if (updateResponse.hasErrors) {
                toast.error(<ToastError errors={updateResponse.errors} />);
                return;
            }
            this.getUsers(this.state.search.skip);
            toast.success(`User ${user.firstName} updated successfully`);
        }
    }

    updateUser = async (user: IUserDto) => {
        if (this.state.isUpdating || window.confirm(`Are you sure to  ${user.isActive ? "Block" : "unblock"} ${user.firstName} ?`)) {
            const { firstName, lastName, emailAddress, gender, role } = user;
            var errors = [];
            if (!Utility.Validate.String(firstName)) {
                errors.push('Please enter first name');
            }
            if (!Utility.Validate.String(lastName)) {
                errors.push('Please enter last name');
            }
            if (!Utility.Validate.String(emailAddress)) {
                errors.push('Please enter email address');
            }
            if (!Utility.Validate.Number(gender)) {
                errors.push('Please select gender');
            }
            if (!Utility.Validate.Number(role)) {
                errors.push('Please select role');
            }
            if (errors.length > 0) {
                toast.error(<ToastError errors={errors} />);
                return;
            }
            var formData = {
                uniqueId: user.uniqueId,
                isActive: (!user.isActive) ?? false,
                firstName: firstName,
                lastName: lastName,
                emailAddress: emailAddress,
                gender: gender,
                role: role
            };
            if (this.state.isUpdating)
                formData.isActive = user.isActive ?? false;
            this.setState({ isLoading: true })
            var updateResponse = await Service.Put<IResult<boolean>>(`${API_END_POINTS.USERS}/${user.uniqueId}`, formData);
            if (this.state.isUpdating) {
                this.setState({
                    isUpdating: false,
                    isLoading: false,
                    user: {
                        ...this.state.user,
                        firstName: '',
                        lastLoginDate: undefined,
                        lastName: '',
                        emailAddress: '',
                        role: 0,
                        gender: 0
                    }
                })
            }
            else
                this.setState({ isLoading: false })
            if (updateResponse.hasErrors) {
                toast.error(<ToastError errors={updateResponse.errors} />);
                return;
            }
            this.getUsers(this.state.search.skip);
            toast.success(`User ${user.firstName} updated successfully`);
        }
    }

    resetUser = () => {
        this.setState({
            isUpdating: false,
            user: {
                firstName: '',
                lastLoginDate: undefined, lastName: '', emailAddress: '',
                gender: 0, role: 0
            }
        })
    }
    render() {
        const { isLoading, users, user, isUpdating } = this.state
        return (
            <div className="card">
                {isLoading ? <Loader /> : null}
                <PageTitle title='Manage Users' />
                <h3 className="card-header">Create &amp; Manage User Accounts</h3>
                <div className="card-body">
                    <form method="post">
                        <div className="row">
                            <div className="col-md-2">
                                <span>First Name</span>
                                <input type="text" placeholder="First Name" className="form-control"
                                    value={user.firstName}
                                    onChange={(e) => { this.setState({ user: { ...this.state.user, firstName: e.target.value } }) }} />
                            </div>
                            <div className="col-md-2">
                                <span>Last Name</span>
                                <input type="text" placeholder="Last Name" className="form-control"
                                    value={user.lastName}
                                    onChange={(e) => { this.setState({ user: { ...this.state.user, lastName: e.target.value } }) }} />
                            </div>
                            <div className="col-md-2">
                                <span>Email Address</span>
                                <input type="text" placeholder="Email Address" className="form-control"
                                    disabled={isUpdating}
                                    value={user.emailAddress}
                                    onChange={(e) => { this.setState({ user: { ...this.state.user, emailAddress: e.target.value.trim() } }) }} />
                            </div>
                            <div className="col-md-2">
                                <span>Gender</span>
                                <select value={user.gender} className="form-control"
                                    onChange={(e) => { this.setState({ user: { ...this.state.user, gender: parseInt(e.target.value) } }) }}>
                                    <option disabled value={0}>Select Gender</option>
                                    <option value={1}>Male</option>
                                    <option value={2}>Female</option>
                                </select>
                            </div>
                            <div className="col-md-2">
                                <span>Role</span>
                                <select value={user.role} className="form-control"
                                    onChange={(e) => { this.setState({ user: { ...this.state.user, role: parseInt(e.target.value) } }) }}>
                                    <option disabled value={0}>Select Role</option>
                                    <option value={1}>Admin</option>
                                    <option value={2}>Member</option>
                                </select>
                            </div>
                            <div className="col-md-2 button-pt">
                                {
                                    isUpdating ? <>
                                        <button type="button" className="btn btn-outline-info" onClick={() => this.updateUser(this.state.user)}> Update User</button>
                                        <button type="button" className="btn btn-outline-danger ms-2"
                                            onClick={() => this.resetUser()}> Reset</button> </>
                                        : <button type="button" className="btn btn-outline-info" onClick={(e) => this.createUser(e)}> Add User</button>
                                }
                            </div>
                        </div>
                    </form>
                    <hr />
                    <div className="row">
                        {
                            users.length > 0 ?
                                <>
                                    <div className="text-end col-12 p-0">
                                        <Paging {...this.state.paging} />
                                    </div>
                                </>
                                : ''
                        }
                    </div>
                    <div className="table-responsive">
                        <table className="table" id="datatable">
                            <thead>
                                <tr>
                                    <th>SR #</th>
                                    <th>First Name</th>
                                    <th>Last Name</th>
                                    <th>Gender</th>
                                    <th>Role</th>
                                    <th>Email Address</th>
                                    <th>Status</th>
                                    <th>Last Login</th>
                                    <th>Created On</th>
                                    <th>Action</th>
                                </tr>
                            </thead>
                            <tbody>
                                {
                                    users.map((user, index) => {
                                        return <tr key={user.uniqueId}>
                                            <td>{this.state.search.skip + index + 1}</td>
                                            <td>{user.firstName}</td>
                                            <td>{user.lastName}</td>
                                            <td>{Gender[user.gender ?? 0]}</td>
                                            <td>{UserRoles[user.role ?? 0]}</td>
                                            <td>{user.emailAddress}</td>
                                            <td><div className={"bg-tags " + (user.isActive ? "bg-success" : "bg-danger")}>{user.isActive ? "Active" : "De-Active"}</div></td>
                                            <td>{Utility.Format.DateTime_DD_MMM_YY_HH_MM_SS(user.lastLoginDate)}</td>
                                            <td>{Utility.Format.DateTime_DD_MMM_YY_HH_MM_SS(user.createdOn)}</td>
                                            <td>
                                                {
                                                    user.isActive ?
                                                        <FaToggleOn onClick={(e) => { this.updateUser(user) }} title="Block User" className='svg-action text-success' /> :
                                                        <FaToggleOff onClick={(e) => { this.updateUser(user) }} title="Unblock User" className='svg-action text-danger' />
                                                }

                                                <AiFillDelete className="svg-action text-danger ms-2" title='Delete User' onClick={() => this.deleteUser(user)} />

                                                <FaEdit className="svg-action text-warning ms-2" title='Edit' onClick={() => this.setState({ isUpdating: true, user: user })} />

                                            </td>
                                        </tr>
                                    })
                                }
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        )
    }
}