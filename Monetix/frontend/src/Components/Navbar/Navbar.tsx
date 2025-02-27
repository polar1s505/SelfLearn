import React from "react";
import logo from "./logo.png";
import "./Navbar.css";
import { Link } from "react-router-dom";

const Navbar = () => {
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

        {/* Login & Signup */}
        <div className="hidden lg:flex items-center space-x-6">
          <a href="#" className="text-black hover:text-darkBlue">Login</a>
          <a href="#" className="px-6 py-2 font-bold rounded-lg text-white bg-lightGreen hover:opacity-80">
            Signup
          </a>
        </div>
      </div>
    </nav>
  );
};

export default Navbar;