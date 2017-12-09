FROM microsoft/aspnetcore-build:2.0 AS builder
RUN apt update -y && apt install -y dos2unix

RUN mkdir /home/appfiles
WORKDIR /home/appfiles
COPY . .

WORKDIR /home/appfiles/
RUN dos2unix build.sh && chmod +x build.sh && sh -e build.sh

FROM microsoft/aspnetcore:2.0
WORKDIR /dist
COPY --from=builder /home/appfiles/dist .
ENTRYPOINT ["dotnet", "AdventOfCode.dll"]