using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Questions : MonoBehaviour {

    public struct QuestionSet
    {
        public string r1, r2, o1, o2, c1, c2;
        public QuestionSet(string input_r1, string input_r2, string input_o1, string input_o2, string input_c1, string input_c2)
        {
            r1 = input_r1;
            r2 = input_r2;
            o1 = input_o1;
            o2 = input_o2;
            c1 = input_c1;
            c2 = input_c2;
        }
    }

    public QuestionSet carmortality = new QuestionSet(
        "What is the range" +
            "\nof South Africa?" +
            "\n(min, max) ",

        "What is the range" +
            "\nof Estonia?" +
            "\n(min, max) ",

        "For 1959" +
            "\nSort the countries" +
            "\nin Ascending order",

        "For 1999" +
            "\nSort the countries" +
            "\nin Ascending order",

        "Consider these 3" +
            "\ncountry-year pairs:" +
            "\n\nEcuador in 2002" +
            "\nFrance in 2003" +
            "\nSouth Africa in 2004" +
            "\n\nWhich one has the" +
            "\nlowest value?",

        "Consider these 3" +
            "\ncountry-year pairs:" +
            "\n\nKorea Rep. in 2000 " +
            "\nCosta Rica in 1997" +
            "\nEcuador in 2002" +
            "\n\nWhich one has the" +
            "\nlowest value?"
        );

    public QuestionSet military = new QuestionSet(
        "What is the range" +
            "\nof Georgia?" +
            "\n(min, max) ",

        "What is the range" +
            "\nof Kuwait?" +
            "\n(min, max) ",

        "For 1999" +
            "\nSort the countries" +
            "\nin Ascending order",

        "For 1997" +
            "\nSort the countries" +
            "\nin Ascending order",

        "Consider these 3" +
            "\ncountry-year pairs:" +
            "\n\nBrunei in 2002" +
            "\nMacedonia in 2004" +
            "\nSudan in 2000" +
            "\n\nWhich one has the" +
            "\nlowest value?",

        "Consider these 3" +
            "\ncountry-year pairs:" +
            "\n\nEthiopia in 1999 " +
            "\nZimbabwe in 2002" +
            "\nBrunei in 1998" +
            "\n\nWhich one has the" +
            "\nlowest value?"
        );

    public QuestionSet agriculturalland = new QuestionSet(
        "What is the range" +
            "\nof Costa Rica?" +
            "\n(min, max) ",

        "What is the range" +
            "\nof Puerto Rico?" +
            "\n(min, max) ",

        "For 1992" +
            "\nSort the countries" +
            "\nin Ascending order",

        "For 2000" +
            "\nSort the countries" +
            "\nin Ascending order",

        "Consider these 3" +
            "\ncountry-year pairs:" +
            "\n\nPuerto Rico in 1992" +
            "\nCosta Rica in 1988" +
            "\nChina in 1984" +
            "\n\nWhich one has the" +
            "\nlowest value?",

        "Consider these 3" +
            "\ncountry-year pairs:" +
            "\n\nComoros in 2004 " +
            "\nGambia in 2000" +
            "\nGrenada in 1968" +
            "\n\nWhich one has the" +
            "\nlowest value?"
        );

    public QuestionSet co2 = new QuestionSet(
        "What is the range" +
            "\nof Bahrain?" +
            "\n(min, max) ",

        "What is the range" +
            "\nof Brunei?" +
            "\n(min, max) ",

        "For 1972" +
            "\nSort the countries" +
            "\nin Ascending order",

        "For 1976" +
            "\nSort the countries" +
            "\nin Ascending order",

        "Consider these 3" +
            "\ncountry-year pairs:" +
            "\n\nQatar, 1988" +
            "\nBahrain, 2000" +
            "\nBahamas, 1976" +
            "\n\nWhich one has the" +
            "\nlowest value?",

        "Consider these 3" +
            "\ncountry-year pairs:" +
            "\n\nBrunei in 1992 " +
            "\nLuxembourg in 2000" +
            "\nBahrain in 1972" +
            "\n\nWhich one has the" +
            "\nlowest value?"
        );
        
    public QuestionSet education = new QuestionSet(
        "What is the range" +
            "\nof Italy?" +
            "\n(min, max)",
        "What is the range" +
            "\nof Netherlands?" +
            "\n(min, max)",
		"For 1990" +
		"\nSort the countries" +
		"\nin Ascending order",
        "For 1991" +
        "\nSort the countries" +
        "\nin Ascending order",
        "Consider these 3" +
            "\ncountry-year pairs:" +
            "\n\nBelgium, 1991" +
            "\nCanada, 1996" +
            "\nItaly, 1993" +
            "\n\nWhich one has the" +
            "\nlowest value?",
        "Consider these 3" +
            "\ncountry-year pairs:" +
            "\n\nCanada in 1991" +
            "\nItaly in 1996" +
            "\nFrance in 1993" +
            "\n\nWhich one has the" +
            "\nlowest value?"
    );
    public QuestionSet grosscapital = new QuestionSet(
        "What is the range" +
            "\nof Suriname?" +
            "\n(min, max)",
        "What is the range" +
            "\nof Chad?" +
            "\n(min, max)",
        "For 1974" +
		"\nSort the countries" +
		"\nin Ascending order",
        "For 1966" +
        "\nSort the countries" +
        "\nin Ascending order",
        "Consider these 3" +
            "\ncountry-year pairs:" +
            "\n\nSwaziland, 1970" +
            "\nCongo Rep., 1978" +
            "\nGreece, 1994" +
            "\n\nWhich one has the" +
            "\nlowest value?",
        "Consider these 3" +
            "\ncountry-year pairs:" +
            "\n\nSwaziland in 1974" +
            "\nLesotho in 1982" +
            "\nGreece in 1966" +
            "\n\nWhich one has the" +
            "\nlowest value?"
    );
    public QuestionSet health = new QuestionSet(
        "What is the range" +
            "\nof Tuvalu?" +
            "\n(min, max)",
        "What is the range" +
            "\nof ?" +
            "\n(min, max)",
        "For 1999" +
        "\nSort the countries" +
        "\nin Ascending order",
        "For " +
        "\nSort the countries" +
        "\nin Ascending order",
        "Consider these 3" +
            "\ncountry-year pairs:" +
            "\n\nRwanda, 2003" +
            "\nZimbabwe, 2001" +
            "\nBotswana, 1998" +
            "\n\nWhich one has the" +
            "\nlowest value?",
        "Consider these 3" +
            "\ncountry-year pairs:" +
            "\n\n" +
            "\n" +
            "\n" +
            "\n\nWhich one has the" +
            "\nlowest value?"
    );
    public QuestionSet homicide = new QuestionSet(
        "What is the range" +
            "\nof Dom. Rep.?" +
            "\n(min, max)",
        "What is the range" +
            "\nof ?" +
            "\n(min, max)",
        "For 1999" +
        "\nSort the countries" +
        "\nin Ascending order",
        "For " +
        "\nSort the countries" +
        "\nin Ascending order",
        "Consider these 3" +
            "\ncountry-year pairs:" +
            "\n\nVenezuela, 1998" +
            "\nEstonia, 2004" +
            "\nKyrgyzstan, 2000" +
            "\n\nWhich one has the" +
            "\nlowest value?",
        "Consider these 3" +
            "\ncountry-year pairs:" +
            "\n\n" +
            "\n" +
            "\n" +
            "\n\nWhich one has the" +
            "\nlowest value?"
    );
    public QuestionSet suicide = new QuestionSet(
        "What is the range" +
            "\nof Canada?" +
            "\n(min, max)",
        "What is the range" +
            "\nof Switzerland?" +
            "\n(min, max)",
        "For 1954" +
        "\nSort the countries" +
        "\nin Ascending order",
        "For 1959" +
        "\nSort the countries" +
        "\nin Ascending order",
        "Consider these 3" +
            "\ncountry-year pairs:" +
            "\n\nBelgium in 1974" +
            "\nNew Zealand in 1984" +
            "\nDenmark in 1999" +
            "\n\nWhich one has the" +
            "\nlowest value?",
        "Consider these 3" +
            "\ncountry-year pairs:" +
            "\n\nBelgium in 1984" +
            "\nDenmark in 1954" +
            "\nFinland in 1974" +
            "\n\nWhich one has the" +
            "\nlowest value?"
    );
    }
