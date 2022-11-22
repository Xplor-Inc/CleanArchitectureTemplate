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
import Visitors from './Pages/Visits';
import './app.css'

interface StateType {
    name: string
    image: string,
    isLoggedIn: boolean
}
const App = () => {
    const profile = useSelector<any, StateType>((state) => state.user)

    var isLogedIn = profile.isLoggedIn;
    return (
        <BrowserRouter>
            <Layout>
                <Routes>
                    {
                        isLogedIn ? //Only add routes if user is logged In.
                            <>
                                <Route path={ROUTE_END_POINTS.USERS} element={<Users />} />
                                <Route path={ROUTE_END_POINTS.CHANGE_PASSWORD} element={<ChangePassword />} />
                                <Route path={ROUTE_END_POINTS.USER_PROFILE} element={<Profile />} />
                                <Route path={ROUTE_END_POINTS.ENQUIRY} element={<Enquiry />} />
                                <Route path={ROUTE_END_POINTS.VISITORS} element={<Visitors />} />
                            </>
                            : null}

                    <Route path={ROUTE_END_POINTS.LOGIN} element={<Login />} />
                    <Route path={ROUTE_END_POINTS.FORGET_PASSWORD} element={<ForgetPassword />} />
                    <Route path={`${ROUTE_END_POINTS.PASSWORD_RECOVERY}/:Id/:Guid/:EmailAddress`} element={<PasswordRecovery />} />
                    <Route path={`${ROUTE_END_POINTS.ACCOUNTACTIVATION}/:Id/:Guid/:EmailAddress`} element={<PasswordRecovery />} />

                </Routes>
            </Layout>
        </BrowserRouter>
    )
}

export default App;
