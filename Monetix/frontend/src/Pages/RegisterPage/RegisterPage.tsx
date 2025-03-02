import React from 'react'
import { yupResolver } from "@hookform/resolvers/yup"
import * as Yup from "yup"
import { useAuth } from '../../Context/userAuth'
import { useForm } from 'react-hook-form'

type Props = {}

type RegisterFormsInput = {
    email: string,
    userName: string,
    password: string,
    confirmPassword: string
}

const validation = Yup.object().shape({
    email: Yup.string()
        .required("Email is required")
        .matches(/^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/, "Invalid email"),
    userName: Yup.string()
        .required("Username is required"),
    password: Yup.string()
        .required("Password is required")
        .min(8, "Password must be at least 8 characters")
        .matches(/(?=.*[0-9])/, "Password must contain at least one digit")
        .matches(/(?=.*[A-Z])/, "Password must contain at least one uppercase letter")
        .matches(/(?=.*[!@#$%^&*(),.?"':;{}|<>])/, "Password must contain at least one special character"),
    confirmPassword: Yup.string()
        .required("Confirm Password is required")
        .oneOf([Yup.ref("password")], "Passwords do not match")
})

const RegisterPage = (props: Props) => {
    const { registerUser } = useAuth()
    const { register, handleSubmit, formState: { errors } } = useForm<RegisterFormsInput>({ resolver: yupResolver(validation) })

    const handleRegister = (form: RegisterFormsInput) => {
        registerUser(form.email, form.userName, form.password, form.confirmPassword)
    }

    return (
        <section className="bg-gray-50 dark:bg-gray-900">
            <div className="flex flex-col items-center justify-center px-6 py-8 mx-auto md:h-screen lg:py-0">
                <div className="w-full bg-white rounded-lg shadow dark:border md:mb-20 sm:max-w-md xl:p-0 dark:bg-gray-800 dark:border-gray-700">
                    <div className="p-6 space-y-4 md:space-y-6 sm:p-8">
                        <h1 className="text-xl font-bold leading-tight tracking-tight text-gray-900 md:text-2xl dark:text-white">
                            Sign Up
                        </h1>
                        <form className="space-y-4 md:space-y-6" onSubmit={handleSubmit(handleRegister)}>
                            <div>
                                <label
                                    htmlFor="email"
                                    className="block mb-2 text-sm font-medium text-gray-900 dark:text-white"
                                >
                                    Email
                                </label>
                                <input
                                    type="text"
                                    id="email"
                                    className="bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
                                    placeholder="Email"
                                    {...register("email")}
                                />
                                {errors.email ? <p>{errors.email.message}</p> : ""}
                            </div>
                            <div>
                                <label
                                    htmlFor="username"
                                    className="block mb-2 text-sm font-medium text-gray-900 dark:text-white"
                                >
                                    Username
                                </label>
                                <input
                                    type="text"
                                    id="username"
                                    className="bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
                                    placeholder="Username"
                                    {...register("userName")}
                                />
                                {errors.userName ? <p>{errors.userName.message}</p> : ""}
                            </div>
                            <div>
                                <label
                                    htmlFor="password"
                                    className="block mb-2 text-sm font-medium text-gray-900 dark:text-white"
                                >
                                    Password
                                </label>
                                <input
                                    type="password"
                                    id="password"
                                    placeholder="••••••••"
                                    className="bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
                                    {...register("password")}
                                />
                                {errors.password ? <p>{errors.password.message}</p> : ""}
                            </div>
                            <div>
                                <label
                                    htmlFor="confirmPassword"
                                    className="block mb-2 text-sm font-medium text-gray-900 dark:text-white"
                                >
                                    Confirm Password
                                </label>
                                <input
                                    type="password"
                                    id="confirmPassword"
                                    placeholder="••••••••"
                                    className="bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
                                    {...register("confirmPassword")}
                                />
                                {errors.confirmPassword ? <p>{errors.confirmPassword.message}</p> : ""}
                            </div>
                            <button
                                type="submit"
                                className="w-full text-white bg-lightGreen hover:bg-primary-700 focus:ring-4 focus:outline-none focus:ring-primary-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center dark:bg-primary-600 dark:hover:bg-primary-700 dark:focus:ring-primary-800"
                            >
                                Sign Up
                            </button>
                        </form>
                    </div>
                </div>
            </div>
        </section>
    )
}

export default RegisterPage