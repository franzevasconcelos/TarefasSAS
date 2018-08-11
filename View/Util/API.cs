using System.Configuration;

namespace View.Util {
    public static class API {
        public static string EnderecoBase() {
            return (string) new AppSettingsReader().GetValue("Endereco.API", typeof(string));
        }

        public static string Login() {
            return EnderecoBase() + "usuario?login=";
        }

    }
}