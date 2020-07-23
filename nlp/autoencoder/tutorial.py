from torch import relu
from torch.utils.data import DataLoader
from torch.nn import Linear, MSELoss, Module
from torch.optim import Adam

import torchvision
import matplotlib.pyplot as plt

transform = torchvision.transforms.Compose([torchvision.transforms.ToTensor()])

train_dataset = torchvision.datasets.MNIST(
    root="~/torch_datasets", train=True, transform=transform, download=True
)

test_dataset = torchvision.datasets.MNIST(
    root="~/torch_datasets", train=False, transform=transform, download=True
)

train_loader = DataLoader(
    train_dataset, batch_size=128, shuffle=True, num_workers=4, pin_memory=True
)

test_loader = DataLoader(
    test_dataset, batch_size=32, shuffle=False, num_workers=4
)

class AE(Module):
  def __init__(self):
    super().__init__()
    self.e_hidden = Linear(in_features=784, out_features=128)
    self.e_output = Linear(in_features=128, out_features=128)
    self.d_hidden = Linear(in_features=128, out_features=128)
    self.d_output = Linear(in_features=128, out_features=784)
    
  def forward(self, features):
    activation = self.e_hidden(features)
    activation = relu(activation)
    code = self.e_output(activation)
    code = relu(code)
    activation = self.d_hidden(code)
    activation = relu(activation)
    activation = self.d_output(activation)
    return relu(activation)

model = AE()

epochs = 20
optimizer = Adam(model.parameters(), lr=1e-3)
criterion = MSELoss()

for epoch in range(epochs):
  loss = 0
  for batch_features, _ in train_loader:

    shape = batch_features[0].shape
    image = batch_features[0].view(shape[1], shape[2], shape[0])
    plt.imshow(image)

    batch_features = batch_features.view(-1, 784)

      

    optimizer.zero_grad()

    outputs = model(batch_features)

    train_loss = criterion(outputs, batch_features)

    train_loss.backward()

    optimizer.step()

    loss += train_loss.item()

  loss = loss / len(train_loader)
  
  print("epoch : {}/{}, loss = {:.6f}".format(epoch+1, epochs, loss))
      
