namespace CatalogoMinimalAPI.Services;
using CatalogoMinimalAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

public class TokenService : ITokenService
{
    public string GerarToken(string key, string issuer, string audience, UserModel user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name,user.UserName),
            new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
        };
        // Este objeto será usado para codificar a chave token.
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
       // encapsula a chave de criptografia e o algoritmo de assinatura utilizado(HMAC - SHA256)
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        // cria um objeto JwtSecurityToken que representa o próprio token 
        var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                signingCredentials: credentials,
                claims: claims,
                expires: DateTime.Now.AddMinutes(120)
            );
        //cria um objeto JwtSecurityTokenHandler e usa-o para gerar uma string JWT a partir do objeto JwtSecurityToken.
        //Essa string representa o token de autenticação que pode ser enviado para o cliente para ser usado em solicitações subsequentes.
        var tokenHendler = new JwtSecurityTokenHandler();
        var stringToken = tokenHendler.WriteToken(token);
        return stringToken;
    }
}
