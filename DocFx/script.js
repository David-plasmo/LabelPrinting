function highlight(selector) {
  var highlighted = document.querySelector("a[name=" + selector + "]");
  var originalColor = highlighted.style.backgroundColor;
  highlighted.style.backgroundColor = "yellow";
  setTimeout(function() {
    highlighted.style.backgroundColor = originalColor;
  }, 1000);
}

function setHighlightCall(selector) {
  return function() {
    highlight(selector);
  };
}

window.onload = function() {
  var anchors = document.querySelectorAll("a[href^='#']");
  anchors.forEach(function(anchor) {
    var anchorName = anchor.href.match(/#([^#]+)$/)[1];
    anchor.onclick = setHighlightCall(anchorName);
  });
};