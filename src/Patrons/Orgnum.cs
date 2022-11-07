﻿namespace TalesOfTribute
{
    public class Orgnum : Patron
    {
        public override bool PatronActivation(Player activator, Player enemy, Card? card = null)
        {
            /*
             * Favored:
             * Pay 3 Coin -> Gain 1 Power for every 4 cards you own, rounded down.
             * Create 1 Maormer Boarding Party card and place it in your cooldown pile.
             * 
             * Neutral:
             * Pay 3 Coin -> Gain 1 Power for every 6 cards that you own, rounded down.
             * 
             * Unfavored:
             * Pay 3 Coin -> Gain 2 Power.
             */

            if (activator.CoinsAmount < 3)
            {
                return false;
            }

            activator.CoinsAmount -= 3;

            int ownerCardsAmount = activator.GetAllPlayersCards().Count;

            if (FavoredPlayer == activator.ID) // Favored
            {
                activator.PowerAmount += ownerCardsAmount / 4;
                activator.CooldownPile.Add(new Parser(cards_config.CARDS_JSON).GetCardByName("Maormer Boarding Party"));
            }
            else if (FavoredPlayer == -1) // Neutral
                activator.PowerAmount += ownerCardsAmount / 6;
            else // Unfavored
                activator.PowerAmount += 2;

            if (FavoredPlayer == -1)
                FavoredPlayer = activator.ID;
            else if (FavoredPlayer == enemy.ID)
                FavoredPlayer = -1;

            return true;
        }

        public override bool PatronPower(Player activator, Player enemy)
        {
            // No benefits

            return true;
        }
    }
}
