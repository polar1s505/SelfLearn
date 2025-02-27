import React, { useEffect, useState } from 'react'
import { CompanyCashFlow } from '../../company';
import { formatLargeMonetaryNumber } from '../../Helpers/NumberFormatting';
import { useOutletContext } from 'react-router-dom';
import { getCashflowStatement } from '../../api';
import Table from '../Table/Table';
import Spinner from '../Spinner/Spinner';

type Props = {}

const config = [
    {
      label: "Date",
      render: (company: CompanyCashFlow) => company.date,
    },
    {
      label: "Operating Cashflow",
      render: (company: CompanyCashFlow) =>
        formatLargeMonetaryNumber(company.operatingCashFlow),
    },
    {
      label: "Investing Cashflow",
      render: (company: CompanyCashFlow) =>
        formatLargeMonetaryNumber(company.netCashUsedForInvestingActivites),
    },
    {
      label: "Financing Cashflow",
      render: (company: CompanyCashFlow) =>
        formatLargeMonetaryNumber(
          company.netCashUsedProvidedByFinancingActivities
        ),
    },
    {
      label: "Cash At End of Period",
      render: (company: CompanyCashFlow) =>
        formatLargeMonetaryNumber(company.cashAtEndOfPeriod),
    },
    {
      label: "CapEX",
      render: (company: CompanyCashFlow) =>
        formatLargeMonetaryNumber(company.capitalExpenditure),
    },
    {
      label: "Issuance Of Stock",
      render: (company: CompanyCashFlow) =>
        formatLargeMonetaryNumber(company.commonStockIssued),
    },
    {
      label: "Free Cash Flow",
      render: (company: CompanyCashFlow) =>
        formatLargeMonetaryNumber(company.freeCashFlow),
    },
    {
      label: "Net Income",
      render: (company: CompanyCashFlow) =>
        formatLargeMonetaryNumber(company.netIncome),
    },
    {
      label: "Net change in cash",
      render: (company: CompanyCashFlow) =>
        formatLargeMonetaryNumber(company.netChangeInCash),
    }
  ];

const CashflowStatement = (props: Props) => {
    const ticker = useOutletContext<string>();
    const [cashflowStatement, setCashflowStatement] = useState<CompanyCashFlow[]>();
    useEffect(() => {
       const getCashflowInit = async () => {
        const value = await getCashflowStatement(ticker!);
        setCashflowStatement(value?.data);
      };
      getCashflowInit();
    }, []);
    return (
      <>
        {cashflowStatement ? (
          <Table config={config} data={cashflowStatement} />
        ) : (
          <Spinner />
        )}
      </>
    );
}

export default CashflowStatement