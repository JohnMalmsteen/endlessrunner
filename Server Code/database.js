// grab the things we need
var mongoose = require('mongoose');
var Schema = mongoose.Schema;


mongoose.connect('mongodb://mugato:#slash93@ds035897.mongolab.com:35897/heroku_app35199285', function(err){
  if(err)console.log("error: "+err);
  else{ console.log("Success");}
});


// create a schema
var userSchema = new Schema({
  username: { type: String, required: true, unique: true },
  score: Number,
  created_at: Date,
  updated_at: Date
});


userSchema.pre('save', function(next) {
  // get the current date
  var currentDate = new Date();

  // change the updated_at field to current date
  this.updated_at = currentDate;

  // if created_at doesn't exist, add to that field
  if (!this.created_at)
    this.created_at = currentDate;

  next();
});


var Scores = mongoose.model('Scores', userSchema);
// make this available to our users in our Node applications
module.exports = Scores;
