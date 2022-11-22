import { useEffect, useState } from "react";
import { toast } from "react-toastify";
import { API_END_POINTS } from "../../Components/Core/Constants/EndPoints";
import { IResult } from "../../Components/Core/Dto/IResultObject";
import { IPagingResult } from "../../Components/Core/Dto/Paging";
import { CounterDto } from "../../Components/Core/Dto/Visits";
import { Service } from "../../Components/Service";
import { Utility } from "../../Components/Service/Utility";
import { Loader } from "../Components/Loader";
import PageTitle from "../Components/Navigation/PageTitle";
import { Paging } from "../Components/Paging";
import { ToastError } from "../Components/ToastError";

const take = 50
var loading = true;
var skip = 0;

const Visitors = () => {
    const nextPage = () => {
        skip = skip + take
        getVisitorsData(skip);
    }

    const prevPage = () => {
        skip = skip - take
        getVisitorsData(skip);
    }

    const [isLoading, SetIsLoading] = useState(true)
    const [paging, SetPaging] = useState<IPagingResult>({ nextPage: nextPage, prevPage: prevPage, nextDisabled: true, prevDisabled: true, text: { count: 0, from: 0, to: 0 } })
    const [sortBy, SetSortBy] = useState("CreatedOn")
    const [sortOrder, SetSortOrder] = useState("DESC")
    const [visits, SetVisits] = useState<CounterDto[]>();

    const getVisitorsData = async (skip: number) => {
        var q = `?skip=${skip}&take=${take}&sortBy=${sortBy}&sortOrder=${sortOrder}`;

        var response = await Service.Get<IResult<CounterDto[]>>(`${API_END_POINTS.COUNTER}${q}`);

        if (response.hasErrors) {
            toast.error(<ToastError errors={response.errors} />)
            SetIsLoading(false)
            return;
        }
        if (response.resultObject) {
            var pagingResult = Service.ApplyPaging(skip, response.rowCount, take);
            SetVisits(response.resultObject)
            SetPaging({
                ...paging,
                nextDisabled: pagingResult.nextDisabled,
                prevDisabled: pagingResult.prevDisabled,
                text: pagingResult.text
            })
            SetIsLoading(false)
        }
        else {
            toast.info('Currently there is no data')
            SetIsLoading(false)
        }
    }

    useEffect(() => {
        if (loading) {
            loading = false;
            getVisitorsData(0)
        }
    }, [visits, paging])

    return <>
        <div className="card">
            {isLoading ? <Loader /> : null}
            <PageTitle title='Visits Counter' />
            <h3 className="card-header">Web traffic</h3>
            <div className="card-body">
                <div className="row">
                    {
                        visits && visits.length > 0 ?
                            <>
                                <div className="text-end col-12 p-0">
                                    <Paging {...paging} />
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
                                <th>Browser</th>
                                <th>Device</th>
                                <th>IP Address</th>
                                <th>OS</th>
                                <th>Page</th>
                                <th>Search</th>
                                <th>Tracking</th>
                                <th>Enquires</th>
                                <th>Created On</th>
                                <th>Last Visit</th>
                            </tr>
                        </thead>
                        <tbody>
                            {
                                visits?.map((visit, index) => {
                                    return <tr key={visit.visitorId}>
                                        <td>{skip + 1 + index}</td>
                                        <td>{visit.browser}</td>
                                        <td>{visit.device}</td>
                                        <td>{visit.ipAddress}</td>
                                        <td>{visit.operatingSystem}</td>
                                        <td>{visit.page}</td>
                                        <td>{visit.search}</td>
                                        <td>{visit.tracking}</td>
                                        <td>{visit.enquirySent}</td>
                                        <td>{Utility.Format.DateTime_DD_MMM_YY_HH_MM_SS(visit.createdOn)}</td>
                                        <td>{Utility.Format.DateTime_DD_MMM_YY_HH_MM_SS(visit.lastVisit)}</td>

                                    </tr>
                                })
                            }
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </>
}
export default Visitors