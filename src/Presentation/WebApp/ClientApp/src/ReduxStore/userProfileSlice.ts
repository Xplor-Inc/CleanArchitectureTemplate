import { createSlice } from '@reduxjs/toolkit'
import { UserRoles } from '../Components/Enums';
import { IUserHookState } from './hooks'

var initialState = {
    isLoggedIn: false,
    name: '',
    image: '/No_image_available.jpg',
    role: UserRoles.Member,
    currentUserId: ''
} as IUserHookState

var p = localStorage.getItem('profile'); // get profile data if exists
var cookie = window.document.cookie.includes('AS');
if (p && cookie) {
    var pObj = JSON.parse(p) as IUserHookState;
    initialState = pObj;
}

export const userProfileSlice = createSlice({
    name: 'profile',
    initialState: initialState,
    reducers: {
        updateProfileState: (state, action) => {
            updateStorage(action.payload)
            state.name = action.payload.name
            state.image = action.payload.image
            state.role = action.payload.role
            state.currentUserId = action.payload.currentUserId 
            state.isLoggedIn = action.payload.isLoggedIn ?? state.isLoggedIn
        }
    }
})

const updateStorage = (data: IUserHookState) => {
    var profile = JSON.stringify(data);
    localStorage.setItem('profile', profile);
}

export const { updateProfileState } = userProfileSlice.actions

export default userProfileSlice.reducer