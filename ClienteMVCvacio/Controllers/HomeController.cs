﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ClienteMVCvacio.Controllers
{
    public class HomeController : Controller
    {
        public string Index()
        {
            return "Hola desde MVC";
        }

        public string Saludar(string nombre = "anonimo")
        {
            return "Hola: " + nombre;
        }
    }
}