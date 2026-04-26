import {Link, NavLink, Outlet} from 'react-router-dom'

export default function Layout (){
return (
    <div className="d-flex flex-column min-vh-100">
        <nav className="navbar navbar-expand-lg navbar-dark bg-primary shadow-sm">
            <div className="container">
                <Link className="navbar-brand fw-semibold" to="/products">Product Demo</Link>
                <button
                    className="navbar-toggler"
                    type="button"
                    data-bs-toggle="collapse"
                    data-bs-target ="#nav"
                    aria-controls="nav"
                    aria-expanded="false"
                    aria-label="Toggle navigation"
                >
                    <span className="navbar-toggler-icon"/>
                </button>
                <div className="collapse navbar-collapse" id="nav">
                    <ul className="navbar-nav ms-auto gap-lg-2">
                        <li className="nav-item">
                            <NavLink className="nav-link" to="/products">Catalog</NavLink>
                        </li>
                        <li className="nav-item">
                            <NavLink className="nav-link" to="/products/create">Add product</NavLink>
                        </li>
                    </ul>
                </div>
            </div>
        </nav>
        <main className="flex-grow-1 py-4">
            <div className="container">
                <Outlet/>
            </div>
        </main>
        <footer className="border-top py-3 text-center text-muted small bg-light">
            AWS Lesson Demo Site {new Date().getFullYear()}
        </footer>
    </div>
)
}