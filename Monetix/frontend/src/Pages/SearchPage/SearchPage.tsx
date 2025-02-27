import React, { ChangeEvent, SyntheticEvent, useState } from 'react'
import { searchCompanies } from '../../api';
import { CompanySearch } from '../../company';
import CardList from '../../Components/CardList/CardList';
import ListPortfolio from '../../Components/Portfolio/ListPortfolio/ListPortfolio';
import Search from '../../Components/Search/Search';

interface Props {}

const SearchPage = (props: Props) => {
    const [search, setSearch] = useState<string>("");
    const [portfolioValues, setPortfolioValues] = useState<string[]>([]);
    const [searchResults, setSearchResults] = useState<CompanySearch[]>([]);
    const [error, setError] = useState<string | null>(null);
    
    const handleSearchChange = (e: ChangeEvent<HTMLInputElement>) => {
        setSearch(e.target.value)
    }
  
    const onPortfolioCreate = async (e: any) => {
      e.preventDefault()
  
      const exists = portfolioValues.find((value) => value === e.target[0].value)
      if(exists) return
      const updatedPortfolio = [...portfolioValues, e.target[0].value]
      setPortfolioValues(updatedPortfolio)
    }
  
    const onPortfolioDelete = (e: any) => {
      e.preventDefault()
  
      const deleted = portfolioValues.filter((value) => {
        return value !== e.target[0].value;
      });
      setPortfolioValues(deleted)
    }
    
    const onSearchSubmit = async (e: SyntheticEvent) => {
      e.preventDefault()
        const result = await searchCompanies(search)
        
        if(typeof result === 'string') {
            setError(result)
        }
        else if(Array.isArray(result)){
            setSearchResults(result)
        }
    }

  return (
    <div className="App">
      <Search onSearchSubmit={onSearchSubmit} search={search} handleSearchChange={handleSearchChange} />
      <ListPortfolio portfolioValues={portfolioValues} onPortfolioDelete={onPortfolioDelete}/>
      <CardList searchResults={searchResults} onPortfolioCreate={onPortfolioCreate}/>
      {error && <h1>{error}</h1>}
    </div>
  )
}

export default SearchPage