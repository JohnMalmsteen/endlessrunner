// set up ========================
var express  = require('express'),
    app = express(),                               // create our app w/ express                     // mongoose for mongodb
    bodyParser = require('body-parser'),   // pull information from HTML POST (express4)
    methodOverride = require('method-override'), // simulate DELETE and PUT (express4)
    Highscores = require('./database.js'),
    md5 = require('MD5');
// configuration ================                                  // log every request to the console
app.use(bodyParser.urlencoded({'extended':'true'}));            // parse application/x-www-form-urlencoded
app.use(bodyParser.json());                                     // parse application/json
app.use(bodyParser.json({ type: 'application/vnd.api+json' })); // parse application/vnd.api+json as json
app.use(methodOverride());

var port = process.env.PORT || 8080;

// routes =======================
// api --------------------------
//get all scores

app.post('/scores', function(req, res){

  if(req.body.app == md5(req.body.username + req.body.score)){
    console.log("in");
    Highscores.find({ username: req.body.username }, function(err, score){
        if(err) throw err;

        if(!score.length){
          console.log("create user");
          Highscores.create({
            username: req.body.username,
            score: req.body.score
          }, function(err, scores){
            if (err) res.send(err);
            findScores(res);
          });
        } else {
          var myScore = score[0];
          if(req.body.score > myScore.score){
            myScore.score = req.body.score;

            myScore.save(function(err) {
              if (err) throw err;

              findScores(res);
              console.log('Score successfully updated!');
            });
          } else {
            findScores(res);
          }

        }
    });
  } else {
    console.log('External request not accepted');
  }
});

function findScores(res){
  Highscores.find().sort({score: -1}).limit(10).exec(function(err, scores) {
      if (err)res.send(err)
      res.json(scores);
  });
}

app.listen(port, function() {
    console.log('Our app is running on http://localhost:' + port);
});
