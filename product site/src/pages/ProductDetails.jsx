import {Link, useParams} from "react-router-dom";
import {useEffect, useState} from "react";
import {getProductById} from "../api/products.js";
import {PLACEHOLDER_IMAGE} from "../config.js";

function formatDate(iso) {
    if(!iso) return "-";
    try {
        return new Date(iso).toLocaleString()
    }
    catch {return iso}
}

export default function ProductDetails(){
    const {id} = useParams();
    const [product, setProduct] = useState(null);
    const [error, setError] = useState(null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        let cancelled = false;
        (async ()=>{
            setLoading(true);
            setError(null);
            try{
                const data = await getProductById(id);
                if(!cancelled)
                    setProduct(data);
            }
            catch(err){
                if(!cancelled)
                    setError(err.message ||"No product found.");
            }
            finally{
                if(!cancelled) setLoading(false);
            }
        })()

        return ()=>{cancelled = true}
    }, [id])

    if(loading){
        return (
            <div className="text-center py-5">
                <div className="spinner-border text-primary" role="status">
                    <span className="visually-hidden">Loading..</span>
                </div>
            </div>
        );
    }

    if(error|| !product){
        return (
            <>
                <div className="alert alert-warning">
                    {error || "Product not found"}
                </div>
                <Link to="/products" className="btn btn-outline-primary">
                    Back to products
                </Link>
            </>
        )
    }

    return (
        <>
            <nav aria-label="breadcrumb" className="mb-3">
                <ol className="breadcrumb mb-0">
                    <li className="breadcrumb-item">
                        <Link to="/products" >
                            Products
                        </Link>
                    </li>
                    <li className="breadcrumb-item active" aria-current="page">
                        {product.name}
                    </li>
                </ol>
            </nav>
            <div className="card h-100 shadow-sm">
                <div className="ratio ratio-4x3 bg-light">
                    <Link
                        to={`/products/${product.id}`}
                        className="d-block h-100 w-100"
                    >
                        <img
                            src={product.imageUrl || PLACEHOLDER_IMAGE}
                            alt={product.name}
                            className="w-100 h-100"
                            style={{ objectFit: "cover" }}
                            onError={(e)=>
                                e.currentTarget.src = PLACEHOLDER_IMAGE}
                        />
                    </Link>
                </div>
                <div className="card-body d-flex flex-column">
                    <h2 className="card-title h5">
                        <Link
                            to={`/products/${product.id}`}
                            className="text-decoration-none text-body"
                        >
                            {product.name}
                        </Link>
                    </h2>
                    <p className="text-muted small mb-2">
                        {product.category || "-"}
                    </p>
                    <p className="fw-semibold text-primary  mb-3">
                        ${Number(product.price).toFixed(2)}
                    </p>
                    <p className="text-muted mb-2">
                        <strong>Discount: </strong>{" "}
                        {product.isDiscountActive ? (
                            <span className="badge text-bg-success">Active</span>
                        ):(<span className="badge text-bg-secondary">Inactive</span>)}
                    </p>
                    <div className="mt-auto d-flex gap-2">
                        <Link
                            to={`/products/edit/${product.id}`}
                            className="btn btn-outline-secondary btn-sm"
                        >
                            Edit
                        </Link>

                    </div>
                </div>
            </div>
        </>
    )
}