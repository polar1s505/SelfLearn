import axios from 'axios';
import { CompanyBalanceSheet, CompanyCashFlow, CompanyHistoricalDividend, CompanyIncomeStatement, CompanyKeyMetrics, CompanyProfile, CompanySearch } from './company';

interface SearchResponse {
    data: CompanySearch[]
};

export const searchCompanies = async (query: string) => {
    try {
        const response = await axios.get<SearchResponse>(
            `https://financialmodelingprep.com/api/v3/search-ticker?query=${query}&limit=1&apikey=${process.env.REACT_APP_API_KEY}`
        );
        return response.data;
    } catch (error) {
        if (axios.isAxiosError(error)) {
            console.error('Axios error:', error.response);
            return error.response;
        } else {
            console.error('Unexpected error:', error);
            return "An unexpected error occurred";
        }
    }
}

export const getCompanyProfile = async (query: string) =>{
    try {
        const data = await axios.get<CompanyProfile[]>(
            `https://financialmodelingprep.com/api/v3/profile/${query}?apikey=${process.env.REACT_APP_API_KEY}`
        )

        return data
    } catch (error: any) {
        console.log("error message from API: ", error.message)
    }
}

export const getKeyMetrics = async (query: string) =>{
    try {
        const data = await axios.get<CompanyKeyMetrics[]>(
            `https://financialmodelingprep.com/api/v3/key-metrics-ttm/${query}?limit=1&apikey=${process.env.REACT_APP_API_KEY}`
        )

        return data
    } catch (error: any) {
        console.log("error message from API: ", error.message)
    }
}

export const getIncomeStatement = async (query: string) =>{
    try {
        const data = await axios.get<CompanyIncomeStatement[]>(
            `https://financialmodelingprep.com/api/v3/income-statement/${query}?limit=1&apikey=${process.env.REACT_APP_API_KEY}`
        )

        return data
    } catch (error: any) {
        console.log("error message from API: ", error.message)
    }
}

export const getBalanceSheet = async (query: string) =>{
    try {
        const data = await axios.get<CompanyBalanceSheet[]>(
            `https://financialmodelingprep.com/api/v3/balance-sheet-statement/${query}?limit=1&apikey=${process.env.REACT_APP_API_KEY}`
        )

        return data
    } catch (error: any) {
        console.log("error message from API: ", error.message)
    }
}

export const getCashflowStatement = async (query: string) =>{
    try {
        const data = await axios.get<CompanyCashFlow[]>(
            `https://financialmodelingprep.com/api/v3/cash-flow-statement/${query}?limit=1&apikey=${process.env.REACT_APP_API_KEY}`
        )

        return data
    } catch (error: any) {
        console.log("error message from API: ", error.message)
    }
}

export const getHistoricalDiv = async (query: string) =>{
    try {
        const data = await axios.get<CompanyHistoricalDividend>(
            `https://financialmodelingprep.com/api/v3/historical-price-full/stock_dividend/${query}?apikey=${process.env.REACT_APP_API_KEY}`
        )

        return data
    } catch (error: any) {
        console.log("error message from API: ", error.message)
    }
}