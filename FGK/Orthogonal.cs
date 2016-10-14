using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGK
{
    class Orthogonal:Camera
    {
        public Vector3 EyePosition { get; set; }
        public double Angle { get; set; }
        public Vector2 CameraSize { get; set; }

        public Orthogonal(Vector3 eye, double angle, Vector2 size)
        {
            this.EyePosition = eye;
            this.Angle = angle;
            this.CameraSize = size;
        }

        public override Ray GetRayTo(Vector2 pictureLocation)
        {
            // Kierunek w którym skierowane są wszystkie promienie
            // wychodzące z kamery.
            // Otrzymany prostymi funkcjami trygonometrycznymi.
            Vector3 direction = new Vector3(
                Math.Sin(Angle),
                0,
                Math.Cos(Angle));

            // Kierunek promienia zawsze musi być znormalizowany.
            direction = direction.Normalized;

            // Jak bardzo początek promienia jest oddalony od 
            // położenia kamery
            Vector2 offsetFromCenter = new Vector2(
                pictureLocation.X * CameraSize.X,
                pictureLocation.Y * CameraSize.Y);

            // Obliczenie finalnego położenia kamery,
            // rówież proste funkcje trygonometryczne.
            Vector3 position = new Vector3(
                EyePosition.X + offsetFromCenter.X * Math.Cos(Angle),
                EyePosition.Y + offsetFromCenter.Y,
                EyePosition.Z + offsetFromCenter.X * Math.Sin(Angle));

            return new Ray(
                position,
                direction);
        }

    }
}
