import axios from "axios"
import { handleError } from "../Helpers/ErrorHandler"
import { UserProfileToken } from "../Models/User"

const api = "https://localhost:7265/api/account"

export const loginAPI = async (userName: string, password: string) => {
    try {
        const data = await axios.post<UserProfileToken>(`${api}/login`, {
            userName: userName,
            password: password
        })

        return data
    } catch (error) {
        handleError(error);
    }
}

export const registerAPI = async (email: string, userName: string, password: string, confirmPassword: string) => {
    try {
        const data = await axios.post(`${api}/account/register`, {
            email: email,
            userName: userName,
            password: password,
            confirmPassword: confirmPassword
        })

        return data
    } catch (error) {
        handleError(error);
    }
}