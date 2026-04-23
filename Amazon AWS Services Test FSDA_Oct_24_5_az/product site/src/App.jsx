import './App.css'
import {Navigate, Route, Routes} from 'react-router-dom'
import Layout from "./components/Layout.jsx";
import ProductsList from "./pages/ProductsList.jsx";
import ProductDetails from "./pages/ProductDetails.jsx";
import ProductEdit from "./pages/ProductEdit.jsx";
import ProductCreate from "./pages/ProductCreate.jsx";
export  default function App(){
  return (
      <Routes>
        <Route path="/" element={<Layout/>}>
          <Route index element={<Navigate to="/products" replace />}/>
          <Route path="products" element={<ProductsList/>}/>
          <Route path="products/edit/:id" element={<ProductEdit/>}/>
          <Route path="products/:id" element={<ProductDetails/>}/>
          <Route path="products/create" element={<ProductCreate/>}/>
        </Route>
      </Routes>
  )
}



