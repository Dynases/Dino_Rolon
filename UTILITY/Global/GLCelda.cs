using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTILITY.Global
{
    public class GLCelda
    {
        public  string campo;
        public  string titulo;
        public  int tamano;
        public  bool visible;
        public  string formato = string.Empty;
        public  void Celda(string Campo, bool Visible, string Titulo, int Tamano, string Formato)
        {
            campo = Campo;
            visible = Visible;
            titulo = Titulo;
            tamano = Tamano;
            formato = Formato;
        }
    }
}
