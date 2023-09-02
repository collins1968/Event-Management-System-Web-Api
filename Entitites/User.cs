﻿namespace Assessment_5.Entitites
{
    public class User
    {
        public Guid Id { get; set; }

        public string Name { get; set; } =string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public List<Event> Events { get; set; } = new List<Event>();
    }
}
