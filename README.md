# YouWrite
YouWrite aims to make writing easier more specially Scientific writing.  The main idea of YouWrite is to create writing environment that is customize the context of writing. 

For that, YouWrite uses local documents (PDFs, papers,books and so on) to index all words and phrases and then suggest the following services: 

- editor with Autocomplete (words bigrams and trigrams).
- Search engine in all the indexed pdfs papers. Most of the papers references are indexed which make the search results more useful by showing exactly the reference. 
- Search the citations of a specific paper Using the indexed references.

# Prequisis
- Dot net 4.0.0 works on windows.

# Install 
- Download Youwrite from  dist/youwrite.exe. 

# Development 

## Used libs : 
* *PDFBOX* : Library used to convert PDF to Text.  [Website](http://www.squarepdf.net/pdfbox-in-net "pdfbox-in-net").
* *OpenNLP* : Library used in NLP. [Website] (https://sharpnlp.codeplex.com/ "sharpnlp"). 
* *Advanced-Text-Editor*: The text editor used on YouWrite. For more information: [Article] (http://www.codeproject.com/Articles/22783/Advanced-Text-Editor-with-Ruler "Advanced-Text-Editor")
* *Autocomplete* : The autocomplete user compnent: [Article](http://www.codeproject.com/Articles/365974/Autocomplete-Menu "Autocomplete-Menu")

## Database 
Sqllite is used as database in Youwrite.




  



