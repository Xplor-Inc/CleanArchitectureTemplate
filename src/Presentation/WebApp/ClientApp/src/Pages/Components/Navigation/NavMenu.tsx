import * as React from 'react';
import { Collapse, Container, Navbar, NavbarToggler, NavItem } from 'reactstrap';
import { NavLink } from 'react-router-dom';
import SessionManager from '../../../Components/Service/SessionManager';
import { ROUTE_END_POINTS } from '../../../Components/Core/Constants/RouteEndPoints';
import NavDropdown from 'react-bootstrap/esm/NavDropdown';
import { FaSignOutAlt } from 'react-icons/fa';
import { ProfileImage } from './ProfileImage';
import { IUserHookState } from '../../../ReduxStore/hooks';
import { useSelector } from 'react-redux';
import { UserRoles } from '../../../Components/Enums';

const NavMenu = () => {
    const profile = useSelector<any, IUserHookState>((state) => state.user)

    const [isOpen, setIsOpen] = React.useState(false);

    const toggle = () => {
        setIsOpen(!isOpen)
    }

    return (
        <header>
            <Navbar className="navbar-expand-sm navbar-toggleable-sm box-shadow fixed-top" light style={{ background: '#1f415f' }}>
                <Container className="box-container">
                    <a href={ROUTE_END_POINTS.HOME} className="navbar-brand">
                        <img src='/Images/icon.jpeg' alt='Logo' style={{borderRadius:'10px'}} />
                    </a>
                    <NavbarToggler onClick={toggle} className="mr-2" />
                    <Collapse className="navbar-collapse" isOpen={isOpen}>
                        <ul className="navbar-nav me-auto mb-2 mb-lg-0">
                            {profile.isLoggedIn && <>
                                
                                {profile.isLoggedIn && profile.role === UserRoles.Admin && <>
                                    <NavItem>
                                        <NavLink className="text-dark nav-link" to={ROUTE_END_POINTS.USERS}>
                                            Users
                                        </NavLink>
                                    </NavItem>
                                </>}
                            </>}
                            {!profile.isLoggedIn && <>
                                <NavItem>
                                    <a className="text-dark nav-link" href={ROUTE_END_POINTS.HOME}>
                                        Home
                                    </a>
                                </NavItem>
                            </>}
                        </ul>
                        <div className="d-flex">
                            <ul className="navbar-nav me-auto mb-2 mb-lg-0">
                                {profile.isLoggedIn &&
                                    <>
                                        <NavDropdown title={<ProfileImage />}>
                                            {profile.isLoggedIn && profile.role === UserRoles.Admin && <>                                               
                                                <NavDropdown.Item className="dropdown-item" role={'button'}>
                                                    <NavLink className="dropdown-item" to={ROUTE_END_POINTS.NOTIFICATIONS}>
                                                        Notifications
                                                    </NavLink>
                                                </NavDropdown.Item>
                                                <NavDropdown.Item className="dropdown-item" role={'button'}>
                                                    <NavLink className="dropdown-item" to={ROUTE_END_POINTS.ENQUIRY}>
                                                        Enquiries
                                                    </NavLink>
                                                </NavDropdown.Item>
                                            <NavDropdown.Divider /></>}
                                            <NavDropdown.Item className="dropdown-item" role={'button'}>
                                                <NavLink className="dropdown-item" to={ROUTE_END_POINTS.CHANGE_PASSWORD}>
                                                    Change Password
                                                </NavLink>
                                            </NavDropdown.Item>
                                            <NavDropdown.Item className="dropdown-item" role={'button'}>
                                                <NavLink className="dropdown-item" role={'button'} to={ROUTE_END_POINTS.USER_PROFILE}>
                                                    Update Profile
                                                </NavLink>
                                            </NavDropdown.Item>
                                            <NavDropdown.Divider />
                                            <NavDropdown.Item href="/profile" onClick={SessionManager.Logout}>
                                                <NavLink className="dropdown-item" role={'button'} to="#logout">
                                                    Logout <FaSignOutAlt />
                                                </NavLink>
                                            </NavDropdown.Item>

                                        </NavDropdown>
                                    </>}
                                {!profile.isLoggedIn &&
                                    <NavItem>
                                        <a className="text-dark nav-link" href="/login">
                                            Login
                                        </a>
                                    </NavItem>}
                            </ul>
                        </div>
                    </Collapse>
                </Container>
            </Navbar>
        </header>
    );
}

export default NavMenu;