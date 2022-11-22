import { useAppSelector } from '../../../ReduxStore/hooks';

export const ProfileImage = () => {
    const profile = useAppSelector(state => state.user);
    return (
        <span>
            {profile.name} &nbsp;
            <img alt={profile.name} width={30} 
            style={{ borderRadius: "50%", height:'35px', width: "35px", border: "1px solid #06bfa6" }}
             src={`/dynamic/images/${profile.image}`} title={profile.name} />
        </span>
    )
}