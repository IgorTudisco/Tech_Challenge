using System;

namespace CloudGame.Domain.Entities;

public sealed class User:Entity<int>
{
    public User(string nome, string email, string password, DateTime birthDate)
    {
        Nome = nome;
        Email = email;
        SetPassword(password);
        BirthDate = birthDate;
    }

    public string Name{get;}

    public string Email{get;}

    public string Password{get;}

    public DateTime BirthDate{get;}

    public bool Active{get;}

    public void SetActive(bool active){
        Active = active;
    }

    public void SetPassword(string password)
    {
        if(password?.Length < 8)
            throw new ArgumentException("A senha precisa ter no minino 8 caracteres.");

        Password = password;
    }

}