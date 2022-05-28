using System;
using System.Collections.Generic;

class Card
{
    public string Suit { get; }
    public string Face { get; }
    public int Value()
    {
        switch (Face)
        {
            case "2":
            case "3":
            case "4":
            case "5":
            case "6":
            case "7":
            case "8":
            case "9":
            case "10":
                return int.Parse(Face);
            case "Jack":
            case "Queen":
            case "King":
                return 10;
            case "Ace":
                return 11;
            default:
                return 0;
        }
    }
    public Card(string newSuit, string newFace)
    {
        Suit = newSuit;
        Face = newFace;
    }
    public string Description()
    {
        return $"{Face} of {Suit}";
    }
}
class Deck
{
    public List<Card> Cards { get; set; }
    public Deck()
    {
        Cards = new List<Card>();
        var suits = new List<string>() { "Hearts", "Spades", "Diamonds", "Clubs" };
        var faces = new List<string>() { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King" };
        foreach (var suit in suits)
        {
            foreach (var face in faces)
            {
                var card = new Card(suit, face);
                Cards.Add(card);
            }
        }
    }
    public void Shuffle()
    {
        var numberOfCards = Cards.Count;
        for (var rightIndex = numberOfCards - 1; rightIndex >= 1; rightIndex--)
        {
            var randomNumberGenerator = new Random();
            var leftIndex = randomNumberGenerator.Next(rightIndex);
            var leftCard = Cards[leftIndex];
            var rightCard = Cards[rightIndex];
            Cards[rightIndex] = leftCard;
            Cards[leftIndex] = rightCard;
        }
    }
    public Card Deal()
    {
        var card = Cards[0];
        Cards.Remove(card);
        return card;
    }
}
namespace blackJack2
{
    class Program
    {
        static void Main(string[] args)
        {
            var continueGame = true;
            while (continueGame)
            {
                var deck = new Deck(); deck.Shuffle();
                Console.WriteLine(deck.Cards.Count);
                var playerHand = new List<Card>();
                playerHand.Add(deck.Deal());
                playerHand.Add(deck.Deal());
                Console.WriteLine(playerHand[0].Description());
                Console.WriteLine(playerHand[1].Description());
                var dealerHand = new List<Card>();
                dealerHand.Add(deck.Deal());
                dealerHand.Add(deck.Deal()); Console.WriteLine(deck.Cards.Count);
                var score = 0;
                foreach (Card card in playerHand)
                {
                    score += card.Value();
                }
                Console.WriteLine($"The score of your hand is {score}");
                var Answer = "n".ToLower();
                do
                {
                    Console.WriteLine("Do you want another card? Y/N");
                    Answer = Console.ReadLine().ToLower();
                    if (Answer.ToLower() == "y")
                    {
                        playerHand.Add(deck.Deal());
                        Console.WriteLine(playerHand[0].Description());
                        Console.WriteLine(playerHand[1].Description());
                        Console.WriteLine(playerHand[2].Description());
                    }
                    score = 0;
                    foreach (Card card in playerHand)
                    {
                        score += card.Value();
                    }
                    Console.WriteLine($"The score of your hand is {score}");
                } while (score <= 21 && Answer.ToLower() == "Y"); if (score > 21)
                {
                    Console.WriteLine("Dealer Wins");
                }
                var dealerScore = 0;
                foreach (Card card in dealerHand)
                {
                    dealerScore += card.Value();
                }
                Console.WriteLine(dealerHand[0].Description());
                Console.WriteLine(dealerHand[1].Description()); while (dealerScore < 17)
                {
                    dealerHand.Add(deck.Deal());
                    dealerScore = 0;
                    foreach (Card card in dealerHand)
                    {
                        dealerScore += card.Value();
                    }
                    Console.WriteLine(dealerHand[dealerHand.Count - 1].Description());
                }
                if (dealerScore > 21)
                {
                    Console.WriteLine("House Loses");
                }
                if (score <= 21 && score > dealerScore)
                {
                    Console.WriteLine("Suck it, I wim");
                }
                else
                {
                    Console.WriteLine("Go home you're drunk");
                }
                Console.WriteLine("Do you want to play again? ");
                continueGame = Console.ReadLine().ToLowerInvariant() == "y";
            }
        }
    }
}