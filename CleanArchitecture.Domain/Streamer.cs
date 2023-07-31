
using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain
{
    public class Streamer : BaseDomainModel
    {

        public string? Nombre { get; set; } 
        public string?  Url { get; set; }

        public ICollection<Video>? Videos { get; set; }//ICOLLECIOTION ES UNA INTERFAZ, AL SER ESTO UNA INTERFAZ NO ESTA IMPLEMENTADO AUN LA COLECCION   
                                                    //SINO QUE SE IMPLEMENTA A LA EJECUCION DEL METODO ESO SIGNIFICA QUE ESTO A  FUTURO PUEDE TRANSFORMARSE EN U HASHSET O LIST

    }
}
