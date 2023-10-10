namespace Blog.External.CrossCutting.Configuration;

public static class ConfigurationJwt
{
    private static string jwtKey = "0xd42c7f0a-0x40ae$0x431a%0xa5-0x6e0xef0x75P@0x72chave0x50xb8$0x88";

    public static string JwtKey { get => jwtKey; set => jwtKey = value; }

}
