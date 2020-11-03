using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SalesSystem.Library
{
    //<T> SIGNIFICA QUE ESTA clase va a recibir cualquier tipo de clase de modelo y va a obtener
    // los datos de la tabla a la que le vamos a preguntar esos registros que tiene
    public class LPaginador<T>
    {
        //cantidad de resutados por página
        private int pagi_cuantos = 10;
        //cantidad de enlaces que se mostrarán como minimo en la barra de navegacíon
        private int pagi_nav_num_enlaces = 3;
        private int pagi_actual;
        //definimos que irá en el enlace a página anterior
        private String pagi_nav_anterior = "&laquo; Anterior ";
        //definimos que irá en el enlace a la página siguiente
        private String pagi_nav_siguiente = " Siguiente &raquo; ";
        //definimos que ira en el enlace a la página siguiente
        private String pagi_nav_primera = " &laquo; Primero ";
        private String pagi_nav_ultima = " Ultimo &raquo; ";
        private String pagi_navegacion = null;


        public object[] paginador(List<T> table, int pagina, int registros, String area, String controller, String action, String host)
        {
            pagi_actual = pagina == 0 ? 1 : pagina;
            if (registros > 0)
            {
                pagi_cuantos = registros;
            }

            int pagi_totalReg = table.Count;
            double valor1 = Math.Ceiling((double)pagi_totalReg / (double)pagi_cuantos);
            int pagi_totalPags = Convert.ToInt16(Math.Ceiling(valor1));

            if(pagi_actual != 1)
            {
                // Si no estamos en la página 1. Ponemos el enlace "primero"
                int pagi_url = 1; //será el numero de página al que enlazamos
                pagi_navegacion += "<a class='btn btn-default' href='" + host + "/" + controller + "/" 
                    + action + "?id=" + pagi_url + "&registros=" + pagi_cuantos + "&area=" + area + "'>" + pagi_nav_primera + "</a>";


                // Si no estamos en la página 1. Ponemos el enlace "anterior"
                pagi_url = pagi_actual - 1; //será el numero de página al que enlazamos
                
            }

            // Si se definió la variable pagi_nav_num_enlaces
            // Calculamos el intervalo para restar y sumar a partir de la pàgina actual
            double valor2 = (pagi_nav_num_enlaces / 2);
            int pagi_nav_intervalo = Convert.ToInt16(Math.Round(valor2));
            // Calculamos dede que numero de pagina se mostrará
            int pagi_nav_desde = pagi_actual - pagi_nav_intervalo;
            // Calculamos hasta que numero de pagina se mostrara
            int pagi_nav_hasta = pagi_actual + pagi_nav_intervalo;
            // Si pagi_nav_desde es un numero negativo
            if (pagi_nav_desde < 1)
            {
                // Le sumamos la cantidad sobrante al final para mantener
                // el numero de enlaces que se quiere mostrar.
                pagi_nav_hasta -= (pagi_nav_desde - 1);
                // Establecemos pagi_nav_desde como 1.
                pagi_nav_desde = 1;
            }

            // Si pagi_nav_hasta es un número mayor que el total de páginas
            if (pagi_nav_hasta > pagi_totalPags)
            {
                // Le restamos la cantidad excedida al comienzo para mantener
                // el numero de enlaces que se quiere mostrar.
                pagi_nav_desde -= (pagi_nav_hasta - pagi_totalPags);
                // Establecemos pagi_nav_hasta como el total de páginas.
                pagi_nav_hasta = pagi_totalPags;
                // Hacemos el último ajuste verificando que al cambiar pagi_nav_desde
                // no haya quedado con un valor no válido.
                if (pagi_nav_desde < 1)
                {
                    pagi_nav_desde = 1;
                }
            }
            for (int pagi_i = pagi_nav_desde; pagi_i <= pagi_nav_hasta; pagi_i++)
            {
                //Desde pagina 1 hasta ultima pagina (pagi_totalPags)
                if (pagi_i == pagi_actual)
                {
                    // Si el numero de pagina es la actual (pagi-actual). Se escribe el número , pero sin enlace y
                    pagi_navegacion += "<span class='btn btn-default' disabled='disabled'>" + pagi_i + "</span>";
                }
                else
                {
                    // Si es caulquier otro. Se escribe el enlace a dicho número de pagina
                   pagi_navegacion += "<a class='btn btn-default' href='" + host + "/" + controller + "/"
                   + action + "?id=" + pagi_i + "&registros=" + pagi_cuantos + "&area=" + area + "'>" + pagi_i + "</a>";
                }
                
            }

            if (pagi_actual < pagi_totalPags)
            {
                // Si no estamos en la última pagina. Ponemos el enlace "Siguiente"
                int pagi_url = pagi_actual + 1; //se el número de página al que enlazamos
                pagi_navegacion += "<a class='btn btn-default' href='" + host + "/" + controller + "/"
                    + action + "?id=" + pagi_url + "&registros=" + pagi_cuantos + "&area=" + area + "'>" + pagi_nav_siguiente + "</a>";

                // Si no estamos en la última página. Ponemos el enlace "Última"
                pagi_url = pagi_totalPags; //será el numero de páginas al que enlazamos
                pagi_navegacion += "<a class='btn btn-default' href='" + host + "/" + controller + "/"
                   + action + "?id=" + pagi_url + "&registros=" + pagi_cuantos + "&area=" + area + "'>" + pagi_nav_ultima + "</a>";

            }

            /*
             Obtener los registros que se van a mostrar en la página principal
             ------------------------------------------------------------------
             */
            // Calculamos desde que registro se mostraá en la página principal
            // El conteo empieza desde CERO
            int pagi_inicial = (pagi_actual - 1) * pagi_cuantos;
            // Consulta SQL. Deveuleve cantidad registros empezando desde pagi_inicial

            var query = table.Skip(pagi_inicial).Take(pagi_cuantos).ToList();

            /*
             * Generación de la información sobre los registros mostrados.
             * -----------------------------------------------------------
             */

            // Números del primer registro de la página actual
            int pagi_desde = pagi_inicial + 1;
            // Número del último registro de la página actual
            int pagi_hasta = pagi_inicial + pagi_cuantos;
            if (pagi_hasta > pagi_totalReg)
            {
                //Si estamos en la última página
                // El último registro de la página actual será igual al número de registros.
                pagi_hasta = pagi_totalReg;
            }

            String pagi_info = " del <b>" + pagi_actual + "</b> al <b>" + pagi_totalPags + "</b> de <b>" +
                pagi_totalReg + "</b> <b>/" + pagi_cuantos + " </b>";

            object[] data = { pagi_info, pagi_navegacion, query };

            return data;
        }

    }
}
