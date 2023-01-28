import { useState, useEffect } from "react";
import { API_END_POINTS } from "../../Components/Core/Constants/EndPoints";
import { IResult } from "../../Components/Core/Dto/IResultObject";
import { INotificationDto } from "../../Components/Core/Dto/Notifications";
import { Service } from "../../Components/Service";

const NotificationMarque = () => {
    const [notifications, setNotifications] = useState<INotificationDto[]>();
    const [close, setCloseNotification] = useState(false)
    useEffect(() => {
        Service.Get<IResult<INotificationDto[]>>(`${API_END_POINTS.NOTIFICATIONS}?showAsNotification=true`).then(response => {
            if (response.resultObject)
                setNotifications(response.resultObject)
            else
                console.log(response);
        })
    }, [])
    const p = notifications?.filter(e => e.isPermanent).length ?? 0;
    return <>{notifications && notifications.length > 0 && !close && <div className="rightCSS li">
        {p === 0 &&
            <span className="closeX" onClick={(e) => { setCloseNotification(true) }}
                style={{ color: 'white', borderColor: 'white',top:'60px' }}>X</span>}
        <div>
            <ul style={{ listStyle: "none", marginBottom:'0.5rem' }}>
                {notifications?.map((m, index) =>
                    <li style={{ float: 'left' }} key={Math.random() }>
                        ★ {m.message} {index === notifications.length - 1 && <span>★</span>}
                    </li>)}
            </ul>
        </div>
    </div>}</>
}

export default NotificationMarque;