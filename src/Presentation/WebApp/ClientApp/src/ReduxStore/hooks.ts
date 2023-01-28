import { TypedUseSelectorHook, useDispatch, useSelector } from 'react-redux'
import { UserRoles } from '../Components/Enums'
import type { RootState, AppDispatch } from './store'

// Use throughout your app instead of plain `useDispatch` and `useSelector`
export interface IUserHookState {
    name: string
    image: string,
    isLoggedIn: boolean,
    role: UserRoles,
    currentUserId:string
}
export const useAppDispatch = () => useDispatch<AppDispatch>()
export const useAppSelector: TypedUseSelectorHook<RootState> = useSelector