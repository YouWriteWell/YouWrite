# YouWrite
YouWrite aims to make writing easier especially for scientific writing.  The main idea of YouWrite is to create an environment that is customized to the context of writing. 

For that, YouWrite uses provided documents (PDFs, papers,books and so on) to index all words and phrases and then suggest the following services: 
- Autocomplete (words bigrams and trigrams).
- Search in all the indexed pdfs papers. Most of the papers references are indexed which make the search results more useful by showing exactly the reference. 
- Search the citations of a specific paper using the indexed references and papers.
#

# Install 
This is the first verison of YouWrite.  

- Download Youwrite 1.0.0.0 installer: [Download](https://github.com/nhaouari/YouWrite/raw/master/dist/YouWrite.exe "YouWrite"). 

# Prerequisites 
- Windows (Not tested for other operating systems).
- Dot net 4.0.0.

# Development 
YouWrite is developed using **Visual Studio C#** community version. 
## Used libs : 
* **PDFBOX** : Library used to convert PDF to Text.  [Website](http://www.squarepdf.net/pdfbox-in-net "pdfbox-in-net").
* **OpenNLP** : Library used in NLP. [Website](https://sharpnlp.codeplex.com/ "sharpnlp"). 
* **Advanced-Text-Editor**: The text editor used on YouWrite. For more information: [Article](http://www.codeproject.com/Articles/22783/Advanced-Text-Editor-with-Ruler "Advanced-Text-Editor")
* **Autocomplete** : The autocomplete user compnent: [Article](http://www.codeproject.com/Articles/365974/Autocomplete-Menu "Autocomplete-Menu")

## Database 
Sqllite is used as database in Youwrite.




  



