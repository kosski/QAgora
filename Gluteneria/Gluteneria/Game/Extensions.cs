using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Gluteneria.Game.Interfaces;
using Gluteneria.MVC.elements;


namespace Gluteneria.Game
{
    public static class Extensions
    {
        public static bool TypeCompare(this object x, object a)
        {
            return x.GetType().IsEquivalentTo(a.GetType());
        }
        public static bool TypeCompare<T>(this object x)
        {
            return x.GetType().IsEquivalentTo(typeof(T));
        }
        public static void IControllerTransUp<TIin, TIout>(this IController<TIout> interfaceOutput, ref IController<TIin> interfaceInput)
            where TIin : TIout where TIout : IElement
        {
            List<TIout> InputListed = interfaceInput.ControledElements.Select(input => (TIout)input).ToList();
            interfaceOutput.ControledElements = InputListed;
        }
        public static void IControllerTransDown<TIin, TIout>(this IController<TIout> interfaceOutput, ref IController<TIin> interfaceInput)
            where TIout : TIin where TIin : IElement
        {
            List<TIout> InputListed = interfaceInput.ControledElements.Select(input => (TIout)input).ToList();
            interfaceOutput.ControledElements = InputListed;
        }
        public static IController<TIout> TransController<TIin, TIout>(this IController<TIout> x, ref IController<TIin> interfaceInput)
            where TIin : IElement where TIout : IElement
        {
            return (IController<TIout>)interfaceInput;
        }
        public static IController<TIout> TransController<TIin, TIout>(this IController<TIin> interfaceInput)
            where TIin : IElement where TIout : IElement
        {
            return (IController<TIout>)interfaceInput;
        }

        public static ParameterInfo[] ParametersType(this object x,string methodName)
        {
            MethodInfo invoke = typeof(object).GetMethod(methodName);
            ParameterInfo[] pars = invoke.GetParameters();
            return pars;
        }
    }
}
