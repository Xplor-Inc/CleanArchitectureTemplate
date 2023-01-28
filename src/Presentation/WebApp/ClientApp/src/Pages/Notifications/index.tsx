import "react-widgets/styles.css";
import { Component } from "react"
import DatePicker from 'react-datepicker';
import { FaEdit } from "react-icons/fa";
import { toast } from "react-toastify";
import { API_END_POINTS } from "../../Components/Core/Constants/EndPoints";
import { IResult } from "../../Components/Core/Dto/IResultObject";
import { Service } from "../../Components/Service";
import { Utility } from "../../Components/Service/Utility";
import { Loader } from "../Components/Loader";
import PageTitle from "../Components/Navigation/PageTitle";
import { ToastError } from "../Components/ToastError";
import { AiFillDelete } from "react-icons/ai";
import { INotificationDto } from "../../Components/Core/Dto/Notifications";
import IBaseStateEnity from "../../Components/Core/PageStates/IBaseStateEnity";

interface INotificationState extends IBaseStateEnity {
    isUpdating: boolean
    notifications: INotificationDto[]
    notification: INotificationDto,
}

export default class NotificationPage extends Component<{}, INotificationState>{
    constructor(props: {}) {
        super(props)

        this.state = {
            isUpdating: false,
            notifications: [],
            isLoading: true,
            notification: {}
        }
    }

    async componentDidMount() {

        this.getNotifications();
    }

    getNotifications = async () => {

        var response = await Service.Get<IResult<INotificationDto[]>>(API_END_POINTS.NOTIFICATIONS);

        if (response.hasErrors) {
            toast.error(<ToastError errors={response.errors} />)
            this.setState({ isLoading: false });
            return;
        }
        if (response.resultObject) {
            this.setState({
                isLoading: false,
                notifications: response.resultObject,
            });
        }
        else {
            toast.info('Currently there is no data')
            this.setState({ isLoading: false });
        }
    }

    createNotification = async (e: React.MouseEvent<HTMLButtonElement>) => {
        const { expireAt, isPermanent, message, startAt } = this.state.notification;
        e.preventDefault();
        var errors = [];

        if (!Utility.Validate.String(message)) {
            errors.push('Please enter message');
        }
        if (!expireAt) {
            errors.push('Please enter expire date');
        }
        if (!startAt) {
            errors.push('Please enter start date');
        }
        if (startAt && expireAt && startAt > expireAt)
            errors.push('Please select valid date');

        if (errors.length > 0) {
            toast.error(<ToastError errors={errors} />);
            return;
        }
        this.setState({ isLoading: true });
        var formData = {
            expireAt: Utility.Format.Date_YYYY_MM_DD(expireAt),
            isPermanent: isPermanent,
            message: message,
            startAt: Utility.Format.Date_YYYY_MM_DD(startAt)
        }
        var response = await Service.Post<IResult<INotificationDto>>(API_END_POINTS.NOTIFICATIONS, formData);

        if (response.hasErrors) {
            toast.error(<ToastError errors={response.errors} />)
            this.setState({
                isLoading: false
            })
            return;
        }
        toast.success(`notification created succssfully`);
        this.setState({
            isLoading: false,
            notification: { expireAt: undefined, isPermanent: false, message: '', startAt: undefined }
        })
        this.getNotifications();
    }

    deleteNotification = async (notification: INotificationDto) => {
        if (window.confirm(`Are you sure to delete notification?`)) {
            this.setState({ isLoading: true })
            var updateResponse = await Service.Delete<IResult<boolean>>(`${API_END_POINTS.NOTIFICATIONS}/${notification.uniqueId}`, {});

            this.setState({ isLoading: false })
            if (updateResponse.hasErrors) {
                toast.error(<ToastError errors={updateResponse.errors} />);
                return;
            }
            this.getNotifications();
            toast.success(`notification deleted successfully`);
        }
    }

    updateNotification = async (e: React.MouseEvent<HTMLButtonElement>) => {
        e.preventDefault();
        const { expireAt, isPermanent, message, startAt, uniqueId } = this.state.notification;
        e.preventDefault();
        var errors = [];

        if (!Utility.Validate.String(message)) {
            errors.push('Please enter message');
        }
        if (!expireAt) {
            errors.push('Please enter expire date');
        }
        if (!startAt) {
            errors.push('Please enter start date');
        }
        if (startAt && expireAt && startAt > expireAt)
            errors.push('Please select valid date');

        if (errors.length > 0) {
            toast.error(<ToastError errors={errors} />);
            return;
        }
        var formData = {
            expireAt: Utility.Format.Date_YYYY_MM_DD(expireAt),
            isPermanent: isPermanent,
            message: message,
            startAt: Utility.Format.Date_YYYY_MM_DD(startAt)
        }
        this.setState({ isLoading: true })
        var updateResponse = await Service.Put<IResult<boolean>>(`${API_END_POINTS.NOTIFICATIONS}/${uniqueId}`, formData);
        if (updateResponse.hasErrors) {
            this.setState({ isLoading: false })
            toast.error(<ToastError errors={updateResponse.errors} />);
            return;
        }
        this.setState({
            isUpdating: false,
            notification: { expireAt: undefined, isPermanent: false, message: '', startAt: undefined }
        })

        this.getNotifications();
        toast.success(`notification updated successfully`);
    }

    render() {
        const { isLoading, notification, notifications, isUpdating } = this.state
        return (
            <div className="card">
                {isLoading ? <Loader /> : null}
                <PageTitle title='Manage Notifications' />
                <h3 className="card-header">Create &amp; Manage Notifications</h3>
                <div className="card-body p-0">
                    <form method="post">
                        <div className="row">
                            <div className="col-md-2">
                                <span>Message</span>
                                <input type="text" placeholder="Enter notification name" className="form-control"
                                    value={notification.message ?? ''}
                                    onChange={(e) => { this.setState({ notification: { ...notification, message: e.target.value } }) }} />
                            </div>
                            <div className="col-md-2">
                                <span>Start Date</span>
                                <DatePicker placeholderText="Select Start Date" className='form-control'
                                    onChange={(date) => {
                                        this.setState({ notification: { ...notification, startAt: Utility.Validate.Date(date?.toString()) } })
                                    }}
                                    selected={Utility.Validate.Date(notification.startAt?.toString())}
                                    value={Utility.Format.Date_DD_MMM_YYYY(notification.startAt) ?? ''}
                                    minDate={new Date()} />
                            </div>
                            <div className="col-md-2">
                                <span>Expire Date</span>
                                <DatePicker placeholderText="Select End Date" className='form-control'
                                    onChange={(date) => {
                                        this.setState({ notification: { ...notification, expireAt: Utility.Validate.Date(date?.toString()) } })
                                    }}
                                    selected={Utility.Validate.Date(notification.expireAt?.toString())}
                                    value={Utility.Format.Date_DD_MMM_YYYY(notification.expireAt) ?? ''}
                                    minDate={notification.startAt} />
                            </div>
                            <div className="col-md-1">
                                <label>Is Permanent</label><br />
                                <div className="form-check  form-check-inline pt-2">
                                    <input type="checkbox" checked={notification.isPermanent}
                                        onChange={(e) => this.setState({ notification: { ...notification, isPermanent: e.target.checked } })}
                                        className="form-check-input" title="Does notification can be tracked on website" />
                                </div>
                            </div>
                            <div className="col-md-4 button-pt">
                                {
                                    !isUpdating ? <button type="button" className="btn btn-primary" onClick={(e) => this.createNotification(e)}> Add notification</button>
                                        : <>
                                            <button type="button" className="btn btn-warning" onClick={(e) => this.updateNotification(e)}> {isUpdating ? "Update" : "Add"} notification</button>
                                            <button type="button" className="btn btn-danger ms-2"
                                                onClick={() => this.setState({
                                                    isUpdating: false,
                                                    notification: { expireAt: undefined, isPermanent: false, message: '', startAt: undefined }
                                                })}> Reset</button>
                                        </>}
                            </div>
                        </div>
                    </form>
                    <hr />

                    <div className="table-responsive">
                        <table className="table" id="datatable">
                            <thead>
                                <tr>
                                    <th>SR #</th>
                                    <th>Start Date</th>
                                    <th>End Date</th>
                                    <th>Message</th>
                                    <th>Can Dismiss</th>
                                    <th>Action</th>
                                </tr>
                            </thead>
                            <tbody>
                                {
                                    notifications.map((notification, index) => {
                                        return <tr key={notification.uniqueId}>
                                            <td>{index + 1}</td>
                                            <td>{Utility.Format.Date_DD_MMM_YYYY(notification.startAt)}</td>
                                            <td>{Utility.Format.Date_DD_MMM_YYYY(notification.expireAt)}</td>
                                            <td>{notification.message}</td>
                                            <td><div style={{ width: '75px' }} className={"bg-tags " + (!notification.isPermanent ? "bg-success" : "bg-warning")}>
                                                {notification.isPermanent ? "No" : "Yes"}</div></td>
                                            <td>
                                                <AiFillDelete className="svg-action me-2" color='red' title='Delete User' onClick={() => this.deleteNotification(notification)} />
                                                <FaEdit className="svg-action text-warning" title='Edit'
                                                    onClick={() => {
                                                        window.scrollTo(0, 0);
                                                        this.setState({
                                                            isUpdating: true, notification: notification
                                                        })
                                                    }} />

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