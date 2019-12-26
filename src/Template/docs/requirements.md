# Featurepakete

## Unsere Aufgabe

* Bei neuer Version einer Abhängigkeit des Featurepakets soll diese automatisiert eingebaut werden
* Jedes Featurepaket hat sein eigenes Repo
* Tooling sollte geteilt/bereitgestellt werden (von wem ist nicht definiert)

## Anforderungen an UIF

### Tool zur Verwaltung von .tcix-Dateien

* Anzeigen von installierten Paketen

        > rps list dependencies ./my.tcix

        Listing dependencies for 'my.tcix':

                                Current:
        FMCController           1.2.3
        HMI_Feedback            0.4.1
        HMI_Touchpoint          21.24.55-pre

* Anzeigen von veralteten Paketen

        > rps list dependencies --outdated ./my.tcix

        Listing outdated dependencies for 'my.tcix':

                                Current:        Latest:
        FMCController           1.2.3           2.0.0
        HMI_Touchpoint          21.24.55-pre    21.25.00

* Updaten von Paketen (Möglichkeit für FBHs auch Patch-Versionen hochzuziehen)

        > rps update dependencies [--minor] [--patch] ./my.tcix
        
        Updated the following dependencies for 'my.tcix':

                                From:           To:
        FMCController           1.2.3           2.0.0
        HMI_Touchpoint          21.24.55-pre    21.25.00

* Im Idealfall ist hier auch der Packer enthalten

        > rps pack ./my.tcix

        ...

* Upload von tciz files zu SCAPS?

        > rps push --component my_component ./my.tciz 

        ...

Analogie zu `dotnet`, `npm`, `choco`, `brew`, ...

Elementares Tooling für Verwender von UIF. Kann überall genutzt werden, unabhängig von Featurepaket.

Ist InstFWcmd dafür geeignet?

* Darf keine Oberfläche haben (Automatisierung)
* Verteilung per PackageManager (Automatisierung)
* Keine Administratorrechte
* Crossplattform

## Anforderungen an SCAPS

1. Webhook für neu hochgeladene Pakete  
Callbackkanal, um Builds automatisiert zu triggern (statt cron-Job)

2. Filterfunktion für ComponentVersions  
Paging? Get latest major/minor/patch?

3. Vollautomatisch gemanagetes Featurepaket in SCAPS  
Macht das Sinn?

## Ergebnis

* UIF: Tool (recht wichtig, wer macht es?)
  * Erstmal wir, in Zusammenspiel mit Joachim
* SCAPS: Webhook (nice to have)
  * Keine konkrete Planung
* SCAPS: Filterung/Paging (nice to have)
  * Keine konkrete Planung
* SCAPS: Automatisch gemanagetes Featurepaket
  * Abgelehnt.
