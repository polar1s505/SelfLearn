import React, { useEffect, useState } from 'react'
import StockCommentForm from './StockCommentForm/StockCommentForm'
import { commentGetAPI, commentPostAPI } from '../../Services/CommentService'
import { toast } from 'react-toastify'
import { useAuth } from '../../Context/userAuth'
import { CommentGet } from '../../Models/Comment'
import Spinner from '../Spinner/Spinner'
import StockCommentList from '../StockCommentList/StockCommentList'

type Props = {
    stockSymbol: string
}

type CommentForInputs = {
    title: string,
    content: string
}

const StockComment = ({stockSymbol}: Props) => {
    const { token } = useAuth()
    const [comments, setComments] = useState<CommentGet[] | null>(null)
    const [loading, setLoading]= useState<boolean>()

    useEffect(() => {
        getComments()
    }, [])

    const handleComment = (e: CommentForInputs) => {
        commentPostAPI(e.title, e.content, stockSymbol, token!)
        .then((res) => {
            if(res){
                toast.success("Comment created successfully!")
                getComments()
            } else{
                toast.warning("Something went wrong:(")
            }
        }).catch((e) => {
            toast.warning(e)
        })
    }

    const getComments = () => {
        setLoading(true)
        commentGetAPI(stockSymbol)
        .then((res) => {
            setLoading(false)
            setComments(res?.data!)
        })
    }

  return (
    <div className="flex flex-col">
        {loading ? <Spinner /> : <StockCommentList comments={comments || []} />}
        <StockCommentForm symbol={stockSymbol} handleComment={handleComment}/>
    </div>
    
  )
}

export default StockComment