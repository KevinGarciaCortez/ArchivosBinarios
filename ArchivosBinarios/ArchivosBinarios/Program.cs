using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ArchivosBinarios
{
    class ArchivoBinarioEmpleados
    {
        BinaryWriter bw = null;
        BinaryReader br = null;

        string Nombre, Direccion;
        long Telefono;
        int NumEmp, DiasTrab;
        float Salario;

        public void CrearA(string Archivo)
        {
            char resp;
            try
            {
                bw = new BinaryWriter(new FileStream(Archivo, FileMode.Create, FileAccess.Write));
                do
                {
                    Console.Clear();
                    Console.Write("Numero de empleados: ");
                    NumEmp=int.Parse(Console.ReadLine());
                    Console.Write("Nombre de Empleado: ");
                    Nombre = Console.ReadLine();
                    Console.Write("Direccion: ");
                    Direccion = Console.ReadLine();
                    Console.Write("Telefono: ");                  
                    Telefono =Int64.Parse(Console.ReadLine());
                    Console.Write("Dias Trabajados: ");
                    DiasTrab = Int32.Parse(Console.ReadLine());
                    Console.Write("Salario Diario: ");
                    Salario = float.Parse(Console.ReadLine());

                    bw.Write(NumEmp);
                    bw.Write(Nombre);
                    bw.Write(Direccion);
                    bw.Write(Telefono);
                    bw.Write(DiasTrab);
                    bw.Write(Salario);

                    Console.Write("\n\nDesea Almacenar otro Registro (s/n)?");
                    resp = Char.Parse(Console.ReadLine());

                } while ((resp=='s')||(resp=='S'));
            }catch (IOException e)
            {
                Console.WriteLine("Error " + e.Message);
            }
            finally
            {
                if(bw != null)bw.Close();
            }
        }
        public void MostrarArchivo(string Archivo)
        {
            try
            {
                if (File.Exists(Archivo))
                {
                    br = new BinaryReader(new FileStream(Archivo, FileMode.Open, FileAccess.Read));
                    Console.Clear();
                    do
                    {
                        NumEmp = br.ReadInt32();
                        Nombre = br.ReadString();
                        Direccion = br.ReadString();
                        Telefono = br.ReadInt64();
                        DiasTrab = br.ReadInt32();
                        Salario = br.ReadSingle();

                        Console.WriteLine("Numero de empleados: " + NumEmp);

                        Console.WriteLine("Nombre de Empleado: " + Nombre);

                        Console.WriteLine("Direccion: " + Direccion);

                        Console.WriteLine("Telefono: " + Telefono);

                        Console.WriteLine("Dias Trabajados: " + DiasTrab);

                        Console.WriteLine("Salario Diario: " + Salario);
                        Console.WriteLine("\n");
                    } while (true);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Archivo inexistente: " + Archivo);
                }
            
            }
            catch (EndOfStreamException)
            {
                Console.WriteLine("\n\nFin del listado");
            }
            finally
            {
                if (br != null) br.Close();
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            string Arch = null;
            int opc;
            ArchivoBinarioEmpleados A1 = new ArchivoBinarioEmpleados();

            do
            {
                Console.Clear();
                Console.WriteLine("\n****Archivo Binaro Empleados****");
                Console.WriteLine("1)Creacion de un Archivo");
                Console.WriteLine("2)Lectura de un Archivo");
                Console.WriteLine("3)Salida del programa");

                Console.Write("Ingrese una opcion: ");
                opc=int.Parse(Console.ReadLine());
                switch (opc)
                {
                    case 1:
                        try
                        {
                            Console.Write("Nombre del archivo: ");
                            Arch=Console.ReadLine();
                            char resp = 's';
                            if (File.Exists(Arch))
                            {
                                Console.WriteLine("Archivo existe desea sobreescribirlo (s/n)");
                                resp = Char.Parse(Console.ReadLine());
                            }
                            if((resp =='s')||(resp=='S'))A1.CrearA(Arch);
                        }
                        catch (IOException e){
                        Console.WriteLine(e.Message);
                        }
                        break;
                        case 2:
                        try
                        {
                            Console.Write("Nombre del archivoa leer: ");
                            Arch = Console.ReadLine();
                            A1.MostrarArchivo(Arch);
                        }
                        catch (IOException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    case 3:
                        Console.Write("Da ENTER para salir");
                        Console.ReadKey();
                        break;
                    default:
                        Console.WriteLine("Opcion incorrecta");
                        break;
                }
            }while(opc!=3);
        }
    }
}
