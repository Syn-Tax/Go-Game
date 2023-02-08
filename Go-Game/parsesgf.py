import sys

with open(sys.argv[1], "r") as f:
  data = f.read()

alpha = "abcdefghijklmnopqrstuvwxyz"
  
for line in data.split("\n"):
  if line != "":
    print(f"b.move({alpha.index(line[1])}, {alpha.index(line[0])}, this);")
  else:
    print("b.move(-1, -1, this);")