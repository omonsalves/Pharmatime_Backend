using Pharmatime_Backend.Models;
using Pharmatime_Backend.Utilities;
using System;

namespace Pharmatime_Backend.Repositories
{
    public class PatientRepository
    {

        public static bool RegisterPatient(CreatePatientDto model)
        {
            Encript e = new Encript();
            Random random = new Random();

            string contraseña = "";

            for (int i = 0; i < 6; i++)
            {
                contraseña += random.Next(0, 10); // Genera un número aleatorio entre 0 y 9
            }

            

            using (var context = new PHARMATIMEContext())
            {
                try
                {
                    var usuario = context.Usuarios.SingleOrDefault(u => u.Correo == model.Correo);

                    if (usuario == null)
                    {
                        var user = new Usuario()
                        {

                            Nombre = model.Nombre,
                            Apellido = model.Apellido,
                            Genero = model.Genero,
                            Telefono = model.Telefono,
                            Edad = model.Edad,
                            Correo = model.Correo,
                            Contraseña = e.EncryptPassword(contraseña),
                            TipoUsuario = 2


                        };

                        context.Usuarios.Add(user);
                        context.SaveChanges();
                        return true;
                    }

                    return false;

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al guardar el usuario: {ex.Message}");
                    return false;
                }

            }
        }

        public static List<object> ObtenerDatosEspecificosDeUsuarios()
        {
            using (var context = new PHARMATIMEContext())
            {
                try
                {
                    var usuarios = context.Usuarios
                        .Select(u => new
                        {
                            Nombre = u.Nombre,
                            Apellido = u.Apellido,
                            Genero = u.Genero,
                            Telefono = u.Telefono,
                            Edad = u.Edad,
                            Correo = u.Correo
                        })
                        .ToList<object>(); // Cambiar a List<object> si se prefiere

                    return usuarios;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al obtener los usuarios: {ex.Message}");
                    return null;
                }
            }
        }



    }
}
