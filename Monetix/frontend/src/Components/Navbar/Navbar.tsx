import React from "react";
import logo from "./logo.png";
import "./Navbar.css";
import { Link } from "react-router-dom";
import { useAuth } from "../../Context/userAuth";

const Navbar = () => {
  const { isLoggedIn, user, logout } = useAuth()

  return (
    <nav className="relative container mx-auto p-4">
      <div className="flex items-center justify-between">
        {/* Logo & Dashboard */}
        <div className="flex items-center space-x-10">
          <Link to="/">
            <img src={logo} alt="Monetix Logo" className="h-10 w-auto" />
          </Link>
          <div className="hidden font-bold lg:flex">
            <Link to="/search" className="text-black hover:text-darkBlue">
              Search
            </Link>
          </div>
        </div>
        {isLoggedIn() ? (
          <div className="hidden lg:flex items-center space-x-6">
            <div className="text-black hover:text-darkBlue">Welcome, {user?.userName}</div>
            <button
              onClick={logout}
              className="px-6 py-2 font-bold rounded-lg text-white bg-lightGreen hover:opacity-80"
            >
              Logout
            </button>
          </div>
        ) : (
          <div className="hidden lg:flex items-center space-x-6">
            <Link to={"/login"} className="text-black hover:text-darkBlue">Login</Link>
            <Link to={"/register"} className="px-6 py-2 font-bold rounded-lg text-white bg-lightGreen hover:opacity-80">
              Signup
            </Link>
          </div>
        )}
      </div>
    </nav>
  );
};

export default Navbar;