using Common;

namespace Year2023.Days;

internal partial class Day7Solver : Solver
{
    public override object SolveChallenge1(string[] input)
    {
        return ParseHands(input, ComputeHandRank1, GetCardRank1);
    }

    public override object SolveChallenge2(string[] input)
    {
        return ParseHands(input, ComputeHandRank2, GetCardRank2);
    }

    private class Hand
    {
        public string Cards { get; set; } = null!;
        public Rank Rank { get; set; }
        public uint Bid { get; set; }

        public int CompareTo(Hand? other, Func<char, int> cardRankFunction)
        {
            if (other == null) return 1;

            if (Rank < other.Rank) return -1;
            if (Rank > other.Rank) return 1;

            for (var i = 0; i < 5; i++)
            {
                var cardRank = cardRankFunction(Cards[i]);
                var otherCardRank = cardRankFunction(other.Cards[i]);

                if (cardRank < otherCardRank) return -1;
                if (cardRank > otherCardRank) return 1;
            }

            return 0;
        }
    }

    private enum Rank
    {
        HighCard = 1,
        OnePair = 2,
        TwoPair = 3,
        ThreeOfAKind = 4,
        FullHouse = 5,
        FourOfAKind = 6,
        FiveOfAKind = 7
    }

    private static object ParseHands(string[] input, Func<Dictionary<char, int>, Rank> handRankFunction, Func<char, int> cardRankFunction)
    {
        var hands = new List<Hand>();

        foreach (var line in input)
        {
            var hand = new Hand();
            hands.Add(hand);

            var components = line.Split(' ');
            hand.Cards = components[0];
            hand.Bid = uint.Parse(components[1]);

            var cardsDict = new Dictionary<char, int>();
            foreach (var card in hand.Cards)
            {
                var count = cardsDict.GetValueOrDefault(card, 0);
                cardsDict[card] = count + 1;
            }

            hand.Rank = handRankFunction(cardsDict);
        }

        hands.Sort((x, y) => x.CompareTo(y, cardRankFunction));

        var total = 0ul;

        for (var i = 0; i < hands.Count; i++)
        {
            total += (uint)(i + 1) * hands[i].Bid;
        }

        return total;
    }

    private static Rank ComputeHandRank1(Dictionary<char, int> cardsDict)
    {
        if (cardsDict.Any(x => x.Value == 5)) return Rank.FiveOfAKind;

        if (cardsDict.Any(x => x.Value == 4)) return Rank.FourOfAKind;

        if (cardsDict.Any(x => x.Value == 3) && cardsDict.Any(x => x.Value == 2)) return Rank.FullHouse;

        if (cardsDict.Any(x => x.Value == 3)) return Rank.ThreeOfAKind;

        if (cardsDict.Count(x => x.Value == 2) == 2) return Rank.TwoPair;

        if (cardsDict.Any(x => x.Value == 2)) return Rank.OnePair;

        return Rank.HighCard;
    }

    private static Rank ComputeHandRank2(Dictionary<char, int> cardsDict)
    {
        var jokerCount = cardsDict.GetValueOrDefault('J', 0);
        if (jokerCount > 0) cardsDict.Remove('J');

        var kind = ComputeHandRank1(cardsDict);

        if (jokerCount == 0) return kind;

        if (kind == Rank.FourOfAKind) return Rank.FiveOfAKind;

        if (kind == Rank.ThreeOfAKind)
        {
            if (jokerCount == 1) return Rank.FourOfAKind;
            if (jokerCount == 2) return Rank.FiveOfAKind;
        }

        if (kind == Rank.TwoPair) return Rank.FullHouse;

        if (kind == Rank.OnePair)
        {
            if (jokerCount == 1) return Rank.ThreeOfAKind;
            if (jokerCount == 2) return Rank.FourOfAKind;
            if (jokerCount == 3) return Rank.FiveOfAKind;
        }

        if (kind == Rank.HighCard)
        {
            if (jokerCount == 1) return Rank.OnePair;
            if (jokerCount == 2) return Rank.ThreeOfAKind;
            if (jokerCount == 3) return Rank.FourOfAKind;
            return Rank.FiveOfAKind;
        }

        return kind;
    }

    private static int GetCardRank1(char card)
    {
        if (card >= '2' && card <= '9') return card - '0';

        return card switch
        {
            'T' => 10,
            'J' => 11,
            'Q' => 12,
            'K' => 13,
            'A' => 14,
            _ => 0
        };
    }

    private static int GetCardRank2(char card)
    {
        if (card >= '2' && card <= '9') return card - '0';

        return card switch
        {
            'T' => 10,
            'Q' => 12,
            'K' => 13,
            'A' => 14,
            'J' => 1,
            _ => 0
        };
    }
}
