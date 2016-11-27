using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGK
{
    public class Transparent:Material
    {
        PhongMaterial direct;
        double refraction;
        double reflection;
        double transmission;
        double specular;
        ColorRgb baseColor;
        public Transparent(ColorRgb color, double diffuse, double specular,
double exponent, double reflection, double refraction, double transmission)
        {
            this.direct = new PhongMaterial(color, diffuse, specular, exponent);
            this.transmission = transmission;
            this.baseColor = color;
            this.reflection = reflection;
            this.specular = specular;
            this.refraction = refraction;
        }
        public override ColorRgb Shade(Raytracer tracer, HitInfo hit)
        {
            ColorRgb final = direct.Shade(tracer, hit);
            Vector3 toCameraDirection = -hit.Ray.Direction;
            double cosIncidentAngle = hit.Normal.Dot(toCameraDirection);
            double eta = cosIncidentAngle > 0 ? refraction : 1 / refraction;
            double refractionCoeff = FindRefractionCoeff(eta, cosIncidentAngle);
            Ray reflectedRay = new Ray(hit.HitPoint, Vector3.Reflect(toCameraDirection, hit.Normal));
            ColorRgb reflectionColor = baseColor * reflection;
            if (IsTotalInternalReflection(refractionCoeff))
            {
                final += tracer.ShadeRay(hit.World, reflectedRay, hit.Depth);
            }
            else
            {
                Ray transmittedRay = ComputeTransmissionDirection(hit.HitPoint, toCameraDirection,
                hit.Normal, eta, Math.Sqrt(refractionCoeff), cosIncidentAngle);
                ColorRgb transmissionColor = ComputeTransmissionColor(eta, hit.Normal, transmittedRay.Direction);
                final += reflectionColor * tracer.ShadeRay(hit.World, reflectedRay, hit.Depth);
                final += transmissionColor * tracer.ShadeRay(hit.World, transmittedRay, hit.Depth);
            }
            return final;
        }
        ColorRgb ComputeTransmissionColor(double eta, Vector3 hitNormal, Vector3 transmittedRayDir)
        {
            return ((ColorRgb.White * transmission) / (eta * eta));
        }
        Ray ComputeTransmissionDirection(Vector3 hitPoint, Vector3 toCameraDirection, Vector3 normal,
double eta, double cosTransmittedAngle, double cosIncidentAngle)
        {
            if (cosIncidentAngle < 0)
            {
                normal = -normal;
                cosIncidentAngle = -cosIncidentAngle;
            }
            Vector3 direction = -toCameraDirection / eta
            - normal * (cosTransmittedAngle - cosIncidentAngle / eta);
            return new Ray(hitPoint, direction);
        }
        double FindRefractionCoeff(double eta, double cosIncidentAngle)
        {
            return 1 - (1 - cosIncidentAngle * cosIncidentAngle) / (eta * eta);
        }
        bool IsTotalInternalReflection(double refractionCoeff)
        {
            return refractionCoeff < 0;
        }

        public override ColorRgb Radiance(Light light, HitInfo hit)
        {
            throw new NotImplementedException();
        }
    }
}
