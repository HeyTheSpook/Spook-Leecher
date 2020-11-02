# Spook-Leecher
A simple c# web-scraper / combo leecher using windows forms and the google search api to make combo lists without the use of sketchy tools, and just has everything you need in one convenient place. I am in the learning stages of learning and much of my code shouldn't be seen as "good practice". Lots of work needs to be done before I would consider this program to be anywhwere near complete, but I would love any feedback or support that you have to offer.

# Completed
-Automated the process of collecting http proxies, as well as makes them completely optional and not needed.
-Completed lists of simple keyword lists that should work for most services that people are leeching for
-Allows the use of custom keywords lists.
-"Properly" returns pastebin urls for pages that appear in the search for keyword
-Added ability to change the date posted in the google api

# To Do
-Add the ability to use more than just pastebin url's
-Add the abilty to use differant search engines other than google (yandex, yahoo, ect)
-Rename the controls to be more readable code
-Fetch the actual combos from pastebins
-Add differant regex's and allow customs ones
-Optimize search code to gather more than 100 links per keywords search
-Optimize efficiency by using more than one thread at a time searching
-Find a way to make the search api not refuse to answer requests for searches(Really don't know about this one. im guessing its bot detection tha ti have to bypass but im not sure)
