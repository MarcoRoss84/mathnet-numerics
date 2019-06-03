using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathNet.Numerics.Providers.DigitalFilter
{
    public static class DigitalFilterControl
    {
        const string EnvVarDFProvider = "MathNetNumericsDFProvider";
        const string EnvVarDFProviderPath = "MathNetNumericsDFProviderPath";

        static IDigitalFilterProvider _digitalFilterProvider;
        static readonly object StaticLock = new object();

        /// <summary>
        /// Gets or sets the linear algebra provider.
        /// Consider to use UseNativeMKL or UseManaged instead.
        /// </summary>
        /// <value>The digital filter provider.</value>
        public static IDigitalFilterProvider Provider
        {
            get
            {
                if (_digitalFilterProvider == null)
                {
                    lock (StaticLock)
                    {
                        if (_digitalFilterProvider == null)
                        {
                            UseDefault();
                        }
                    }
                }

                return _digitalFilterProvider;
            }
            set
            {
                value.InitializeVerify();

                // only actually set if verification did not throw
                _digitalFilterProvider = value;
            }
        }

        /// <summary>
        /// Optional path to try to load native provider binaries from.
        /// If not set, Numerics will fall back to the environment variable
        /// `MathNetNumericsLAProviderPath` or the default probing paths.
        /// </summary>
        public static string HintPath { get; set; }

        public static IDigitalFilterProvider CreateManaged()
        {
            return new Managed.ManagedDigitalFilterProvider();
        }

        public static void UseManaged()
        {
            Provider = CreateManaged();
        }

        internal static IDigitalFilterProvider CreateManagedReference()
        {
            return new Managed.ManagedDigitalFilterProvider();
        }

        internal static void UseManagedReference()
        {
            Provider = CreateManagedReference();
        }

#if NATIVE
        [CLSCompliant(false)]
        public static IDigitalFilterProvider CreateNativeMKL(
            Common.Mkl.MklConsistency consistency = Common.Mkl.MklConsistency.Auto,
            Common.Mkl.MklPrecision precision = Common.Mkl.MklPrecision.Double,
            Common.Mkl.MklAccuracy accuracy = Common.Mkl.MklAccuracy.High)
        {
            return new Mkl.MklDigitalFilterProvider(GetCombinedHintPath(), consistency, precision, accuracy);
        }

        [CLSCompliant(false)]
        public static void UseNativeMKL(
            Common.Mkl.MklConsistency consistency = Common.Mkl.MklConsistency.Auto,
            Common.Mkl.MklPrecision precision = Common.Mkl.MklPrecision.Double,
            Common.Mkl.MklAccuracy accuracy = Common.Mkl.MklAccuracy.High)
        {
            Provider = CreateNativeMKL(consistency, precision, accuracy);
        }

        [CLSCompliant(false)]
        public static bool TryUseNativeMKL(
            Common.Mkl.MklConsistency consistency = Common.Mkl.MklConsistency.Auto,
            Common.Mkl.MklPrecision precision = Common.Mkl.MklPrecision.Double,
            Common.Mkl.MklAccuracy accuracy = Common.Mkl.MklAccuracy.High)
        {
            return TryUse(CreateNativeMKL(consistency, precision, accuracy));
        }

       
        /// <summary>
        /// Try to use a native provider, if available.
        /// </summary>
        public static bool TryUseNative()
        {
            return TryUseNativeMKL();
        }
#endif

        static bool TryUse(IDigitalFilterProvider provider)
        {
            try
            {
                if (!provider.IsAvailable())
                {
                    return false;
                }

                Provider = provider;
                return true;
            }
            catch
            {
                // intentionally swallow exceptions here - use the explicit variants if you're interested in why
                return false;
            }
        }

        /// <summary>
        /// Use the best provider available.
        /// </summary>
        public static void UseBest()
        {
#if NATIVE
            if (!TryUseNative())
            {
                UseManaged();
            }
#else
            UseManaged();
#endif
        }

        /// <summary>
        /// Use a specific provider if configured, e.g. using the
        /// "MathNetNumericsLAProvider" environment variable,
        /// or fall back to the best provider.
        /// </summary>
        public static void UseDefault()
        {
#if NATIVE
            var value = Environment.GetEnvironmentVariable(EnvVarDFProvider);
            switch (value != null ? value.ToUpperInvariant() : string.Empty)
            {
                case "MKL":
                    UseNativeMKL();
                    break;

                default:
                    UseBest();
                    break;
            }
#else
            UseBest();
#endif
        }

        public static void FreeResources()
        {
            Provider.FreeResources();
        }

        static string GetCombinedHintPath()
        {
            if (!String.IsNullOrEmpty(HintPath))
            {
                return HintPath;
            }

            var value = Environment.GetEnvironmentVariable(EnvVarDFProviderPath);
            if (!String.IsNullOrEmpty(value))
            {
                return value;
            }

            return null;
        }
    }
}
