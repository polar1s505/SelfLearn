import React from 'react'
import Table from '../../Components/Table/Table'
import RatioList from '../../Components/RatioList/RatioList'
import { formatLargeNonMonetaryNumber } from '../../Helpers/NumberFormatting'
import { CompanyKeyMetrics } from '../../company'
import { testIncomeStatementData } from '../../Components/Table/testData'

interface Props { }

const tableConfig = [
  {
    label: "Market Cap",
    render: (company: CompanyKeyMetrics) =>
      formatLargeNonMonetaryNumber(company.marketCapTTM),
    subTitle: "Total value of all a company's shares of stock",
  },
]

const DesignPage = (props: Props) => {
    return (
        <>
            <h1>Monetix Design Page</h1>
            <h2>
                This is Monetix's design page. This is where we will store varios design aspects of the app
            </h2>
            <RatioList data={testIncomeStatementData} config={tableConfig}/>
            <Table config={tableConfig} data={testIncomeStatementData}/>
        </>
    )
}

export default DesignPage