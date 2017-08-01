var gulp = require("gulp");
var rimraf = require("rimraf");
var fs = require("fs");
var sass = require("gulp-sass");
var webroot = "./wwwroot";


gulp.task("sass", function () {
    gulp.src('*.scss')
      .pipe(sass())
      .pipe(gulp.dest(function(f){
          return f.base;
      }))
});

gulp.task('default', ['sass'], function () {
    gulp.watch('*.scss', ['sass']);
});