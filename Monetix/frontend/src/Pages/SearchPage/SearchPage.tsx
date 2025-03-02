import React, { ChangeEvent, SyntheticEvent, useEffect, useState } from 'react'
import { searchCompanies } from '../../api';
import { CompanySearch } from '../../company';
import CardList from '../../Components/CardList/CardList';
import ListPortfolio from '../../Components/Portfolio/ListPortfolio/ListPortfolio';
import Search from '../../Components/Search/Search';
import { PortfolioGet } from '../../Models/Portfolio';
import { portfolioAddAPI, portfolioDeleteAAPI, portfolioGetAPI } from '../../Services/PortfolioService';
import { toast } from 'react-toastify';

interface Props { }

const SearchPage = (props: Props) => {
  const [search, setSearch] = useState<string>("");
  const [portfolioValues, setPortfolioValues] = useState<PortfolioGet[] | null>([]);
  const [searchResults, setSearchResults] = useState<CompanySearch[]>([]);
  const [error, setError] = useState<string | null>(null);

  const handleSearchChange = (e: ChangeEvent<HTMLInputElement>) => {
    setSearch(e.target.value)
  }

  useEffect(() => {
    getPortfolio()
  }, [])

  const getPortfolio = () => {
    portfolioGetAPI()
      .then((res) => {
        if (res?.data) {
          setPortfolioValues(res?.data)
        }
      }).catch((e) => {
        toast.warning("Could not get portfolio values!")
      })
  }

  const onPortfolioCreate = async (e: any) => {
    e.preventDefault()
    portfolioAddAPI(e.target[0].value)
    .then((res) => {
      if(res?.status === 201) {
        toast.success("Stock added to portfolio!")
        getPortfolio()
      }
    }).catch((e) => {
      toast.warning("Could not create portfolio item!")
    })
  }

  const onPortfolioDelete = (e: any) => {
    e.preventDefault()
    portfolioDeleteAAPI(e.target[0].value)
    .then((res) => {
      if(res?.status === 204) {
        toast.success("Stock deleted from portfolio!")
        getPortfolio()
      }
    }).catch((e) => {
      toast.warning("Could not delete item from portfolio!")
    })
  }

  const onSearchSubmit = async (e: SyntheticEvent) => {
    e.preventDefault()
    const result = await searchCompanies(search)

    if (typeof result === 'string') {
      setError(result)
    }
    else if (Array.isArray(result)) {
      setSearchResults(result)
    }
  }

  return (
    <div className="App">
      <Search onSearchSubmit={onSearchSubmit} search={search} handleSearchChange={handleSearchChange} />
      <ListPortfolio portfolioValues={portfolioValues!} onPortfolioDelete={onPortfolioDelete} />
      <CardList searchResults={searchResults} onPortfolioCreate={onPortfolioCreate} />
      {error && <h1>{error}</h1>}
    </div>
  )
}

export default SearchPage