import { useEffect, useState } from 'react';
import { API_END_POINTS } from '../../Components/Core/Constants/EndPoints';
import { IChangeLogDto } from '../../Components/Core/Dto/Audits/IChangeLog';
import { IResult } from '../../Components/Core/Dto/IResultObject';
import { Service } from '../../Components/Service';
import { Utility } from '../../Components/Service/Utility';

const ChangeLogAuditComponent = ({ entityName, primaryKey }: { primaryKey: number, entityName: string }) => {
    const [changeLogs, setChangeLogs] = useState<IChangeLogDto[]>();

    useEffect(() => {
        Service.Get<IResult<IChangeLogDto[]>>(`${API_END_POINTS.CHANGELOGS}/?primaryKey=${primaryKey}&entityName=${entityName}`).then(response => {
            if (response.resultObject && response.resultObject.length > 0)
                setChangeLogs(response.resultObject)
        })
    }, [])
    return (
        <> {changeLogs && changeLogs.length > 0 &&
            <>
                <div className="card-header">
                    <span className='h5'>Update History
                    </span>
                </div>
                <div className="card-body">
                    <div className="table-responsive">
                        <table className="table">
                            <thead>
                                <tr>
                                <th>Update Date</th>
                                <th>Change By</th>
                                    <th>Property</th>
                                    <th>Old Value</th>
                                    <th>New Value</th>
                                </tr>
                            </thead>
                            <tbody>
                                {changeLogs.map((log) => {
                                    return <tr key={log.id}>
                                        <td>{Utility.Format.DateTime_DD_MMM_YY_HH_MM_SS(log.createdOn)}</td>
                                        <td>{log.createdBy?.firstName} {log.createdBy?.lastName}</td>
                                        <td>{log.propertyName}</td>
                                        <td>{log.oldValue ?? "--"}</td>
                                        <td>{log.newValue ?? "--"}</td>
                                    </tr>
                                })}
                            </tbody>
                        </table>
                    </div>
                </div>
            </>
        }
        </>
    );
}

export default ChangeLogAuditComponent;