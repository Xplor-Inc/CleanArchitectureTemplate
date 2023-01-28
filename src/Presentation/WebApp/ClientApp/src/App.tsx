import { Route, Routes } from 'react-router';
import { BrowserRouter } from 'react-router-dom';
import Layout from './Pages/Components/Navigation/Layout';
import Login from './Pages/Account/Login';
import ForgetPassword from './Pages/Account/ForgetPassword';
import { ROUTE_END_POINTS } from './Components/Core/Constants/RouteEndPoints';
import PasswordRecovery from './Pages/Account/PasswordRecovery';
import ChangePassword from './Pages/Account/ChangePassword';
import Users from './Pages/Users';
import Profile from './Pages/Profile';
import Enquiry from './Pages/Enquiry';
import { useSelector } from 'react-redux';
import './app.css'
import NotificationMarque from './Pages/Components/NotificationMarque';
import NotificationPage from './Pages/Notifications';
import { IUserHookState } from './ReduxStore/hooks';
import { UserRoles } from './Components/Enums';
import NotFoundPage from './Pages/NotFoundPage';

const App = () => {
    const profile = useSelector<any, IUserHookState>((state) => state.user)

    return (
        <BrowserRouter>
            <Layout>
                {profile.isLoggedIn && <NotificationMarque />}
                <Routes>
                    {profile.isLoggedIn &&
                        <>
                            <Route path={ROUTE_END_POINTS.CHANGE_PASSWORD} element={<ChangePassword />} />
                            <Route path={ROUTE_END_POINTS.USER_PROFILE} element={<Profile />} />
                        </>}
                    {profile.isLoggedIn && profile.role === UserRoles.Admin &&
                        <>
                            <Route path={ROUTE_END_POINTS.USERS} element={<Users />} />
                            <Route path={ROUTE_END_POINTS.NOTIFICATIONS} element={<NotificationPage />} />
                            <Route path={ROUTE_END_POINTS.ENQUIRY} element={<Enquiry />} />
                        </>
                    }
                    <Route path={ROUTE_END_POINTS.LOGIN} element={<Login />} />
                    <Route path={ROUTE_END_POINTS.FORGET_PASSWORD} element={<ForgetPassword />} />
                    <Route path={`${ROUTE_END_POINTS.PASSWORD_RECOVERY}/:Id/:Guid/:EmailAddress`} element={<PasswordRecovery />} />
                    <Route path={`${ROUTE_END_POINTS.ACCOUNTACTIVATION}/:Id/:Guid/:EmailAddress`} element={<PasswordRecovery />} />
                    <Route path='*' element={<NotFoundPage />} />
                </Routes>
            </Layout>
        </BrowserRouter>
    )
}

export default App;
