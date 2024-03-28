using Microsoft.EntityFrameworkCore;
using Pharmatime_Backend.Models;
using Pharmatime_Backend.Utilities;
using System;

public class UserRepository
{

    public static bool Register(RegisterDto model)
    {
        Encript e = new Encript();
        
        using (var context = new PHARMATIMEContext())
        {
            try
            {
                var usuario = context.Usuarios.SingleOrDefault(u => u.Correo == model.Correo);

                if(usuario == null) {
                    var user = new Usuario()
                    {

                        Nombre = model.Nombre,
                        Apellido = model.Apellido,
                        Genero = model.Genero,
                        Telefono = model.Telefono,
                        Edad = model.Edad,
                        Correo = model.Correo,
                        Contraseña = e.EncryptPassword(model.Contraseña)

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


    public static bool Login(string correo, string contraseña)
    {
        Encript e = new Encript();

        using (var context = new PHARMATIMEContext())
        {
            try
            {
                var usuario = context.Usuarios.SingleOrDefault(u => u.Correo == correo && u.Contraseña == e.EncryptPassword(contraseña));

                if (usuario != null)
                {
                    // El usuario con el correo y la contraseña proporcionados existe en la base de datos.
                    return true;
                }
                else
                {
                    // No se encontró un usuario con el correo y la contraseña proporcionados.
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al consultar el usuario: {ex.Message}");
                return false;
            }
        }
    }


}
