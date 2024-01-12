# Changelog
All notable changes to this project will be documented in this file. See [conventional commits](https://www.conventionalcommits.org/) for commit guidelines.

- - -
## [v0.2.3](https://github.com/brad-jones/dotnetup/compare/v0.2.2..v0.2.3) - 2024-01-12
#### Bug Fixes
- dotnet tools env var path - ([0ab4e18](https://github.com/brad-jones/dotnetup/commit/0ab4e18a72f66631a277e48d03fc706697d9a2b6)) - [@brad-jones](https://github.com/brad-jones)

- - -

## [v0.2.2](https://github.com/brad-jones/dotnetup/compare/v0.2.1..v0.2.2) - 2024-01-12
#### Bug Fixes
- **(win)** env vars need to be given in the hosts native format - ([bf5eed5](https://github.com/brad-jones/dotnetup/commit/bf5eed586217f606b618930cee98f0eaeb9b00f8)) - [@brad-jones](https://github.com/brad-jones)

- - -

## [v0.2.1](https://github.com/brad-jones/dotnetup/compare/v0.2.0..v0.2.1) - 2024-01-12
#### Bug Fixes
- resolve symlinked dotnet roots - ([f07d613](https://github.com/brad-jones/dotnetup/commit/f07d6133484c4a1b192b396d2b8d5f3214a4df3d)) - [@brad-jones](https://github.com/brad-jones)

- - -

## [v0.2.0](https://github.com/brad-jones/dotnetup/compare/v0.1.5..v0.2.0) - 2024-01-11
#### Features
- many critical improvements - ([b7c19a4](https://github.com/brad-jones/dotnetup/commit/b7c19a4a4213ba992347980a1e208ed8cf6b3e1a)) - [@brad-jones](https://github.com/brad-jones)

- - -

## [v0.1.5](https://github.com/brad-jones/dotnetup/compare/v0.1.4..v0.1.5) - 2024-01-11
#### Bug Fixes
- **(package)** the env var json file needs to be listed in the paths json - ([f8811dc](https://github.com/brad-jones/dotnetup/commit/f8811dc6a7a3ac2281b288cd9d8aa356f721ce50)) - [@brad-jones](https://github.com/brad-jones)

- - -

## [v0.1.4](https://github.com/brad-jones/dotnetup/compare/v0.1.3..v0.1.4) - 2024-01-11
#### Bug Fixes
- **(package)** logic error in optional chmod - ([da95773](https://github.com/brad-jones/dotnetup/commit/da9577356eba4e81c707eb0caaa12af10dc42487)) - [@brad-jones](https://github.com/brad-jones)
- **(publish)** minor changes to logging - ([b89de93](https://github.com/brad-jones/dotnetup/commit/b89de93e3b799888c87b378b9f0de262fa4659e6)) - [@brad-jones](https://github.com/brad-jones)

- - -

## [v0.1.3](https://github.com/brad-jones/dotnetup/compare/v0.1.2..v0.1.3) - 2024-01-11
#### Bug Fixes
- **(pixi)** publish conda packages to prefix.dev - ([5c0b4cc](https://github.com/brad-jones/dotnetup/commit/5c0b4cc6cbdd2fd55e399201ab09e873f6d3fa6b)) - [@brad-jones](https://github.com/brad-jones)
#### Continuous Integration
- **(cog)** next version logic is broken - ([d29588e](https://github.com/brad-jones/dotnetup/commit/d29588ed8d3136b9fba814d972097247cfcf8bfd)) - [@brad-jones](https://github.com/brad-jones)
- **(publish)** split package & publish into 2 steps - ([119bd21](https://github.com/brad-jones/dotnetup/commit/119bd21e01f846617336e91d90151490dcf65661)) - [@brad-jones](https://github.com/brad-jones)
- **(publish)** go-task was caching the dynamic HASH var - ([0299bb9](https://github.com/brad-jones/dotnetup/commit/0299bb9f69218200009b638f53f6dd88837d4251)) - [@brad-jones](https://github.com/brad-jones)

- - -

## [v0.1.2](https://github.com/brad-jones/dotnetup/compare/v0.1.1..v0.1.2) - 2024-01-11
#### Bug Fixes
- **(publish)** remove publish dir from checksums file - ([919a646](https://github.com/brad-jones/dotnetup/commit/919a64626dc3d505725dde82b376582a29d2b558)) - [@brad-jones](https://github.com/brad-jones)

- - -

## [v0.1.1](https://github.com/brad-jones/dotnetup/compare/v0.1.0..v0.1.1) - 2024-01-11
#### Bug Fixes
- **(arm64)** forgot to publish arm64 binaries - ([d47a533](https://github.com/brad-jones/dotnetup/commit/d47a533262c3b97d50a371e15046de0db76df71e)) - [@brad-jones](https://github.com/brad-jones)
#### Continuous Integration
- **(cog)** run dprint on generated changelog - ([4bf4a60](https://github.com/brad-jones/dotnetup/commit/4bf4a6074de9f388b677e39149cdab61e1727923)) - [@brad-jones](https://github.com/brad-jones)
#### Revert
- **(cog)** turns out that the format of the CHANGELOG is important to cog so we will just ignore it from dprint instead - ([8ca46d7](https://github.com/brad-jones/dotnetup/commit/8ca46d7ccd81af07c5c14af016b33dd5aefaa0a4)) - [@brad-jones](https://github.com/brad-jones)

- - -

## [v0.1.0](https://github.com/brad-jones/dotnetup/compare/b5a084e6c462cbadafac4005949d762a056fda28..v0.1.0) - 2024-01-11
#### Build system
- **(lefthook)** add glob filters to dotnet fmt tasks - ([6fbdf0c](https://github.com/brad-jones/dotnetup/commit/6fbdf0c1fbffad3804d5cb6824a11c9630f540c8)) - [@brad-jones](https://github.com/brad-jones)
- strip v prefix before we give it to dotnet publish - ([78718b8](https://github.com/brad-jones/dotnetup/commit/78718b8586f37d6139c66d4475fb99d4ff78a446)) - [@brad-jones](https://github.com/brad-jones)
#### Continuous Integration
- **(build_test)** also build self contained x64 - ([5c977c8](https://github.com/brad-jones/dotnetup/commit/5c977c8bd266a8a6e234399eae8f17f7178f8611)) - [@brad-jones](https://github.com/brad-jones)
- **(build_test)** spit out builds into seperate artifacts - ([70525ab](https://github.com/brad-jones/dotnetup/commit/70525ab12724620a337e1c7e39b07749733ed4ab)) - [@brad-jones](https://github.com/brad-jones)
- **(build_test)** clean can just be a local task for when things get out of hand - ([daaa2a1](https://github.com/brad-jones/dotnetup/commit/daaa2a1392580a66d1cda0ed9c4432b8826f6fd0)) - [@brad-jones](https://github.com/brad-jones)
- **(build_test)** debug win & osx outputs - ([bc86343](https://github.com/brad-jones/dotnetup/commit/bc86343a8dd3435fedf5fe2cdc358556bd42c8a7)) - [@brad-jones](https://github.com/brad-jones)
- **(build_test)** matrix job for each os - ([473c18e](https://github.com/brad-jones/dotnetup/commit/473c18eae05cadc1a2a55f1d1ad9ba226e4746d9)) - [@brad-jones](https://github.com/brad-jones)
- **(cog)** final tweaks to cog so that hopefully we get a good release - ([686cf12](https://github.com/brad-jones/dotnetup/commit/686cf129a97c44537c9dd89fd3377803ef409f7f)) - [@brad-jones](https://github.com/brad-jones)
- **(cog)** need to set git identity before publish - ([e7af533](https://github.com/brad-jones/dotnetup/commit/e7af533f4900e2e0f9bf3dc5eb6d4ce11e4f196d)) - [@brad-jones](https://github.com/brad-jones)
- **(dprint)** ignore tools dir - ([4796997](https://github.com/brad-jones/dotnetup/commit/4796997a0e2d8f34eed998e262a5f34405661362)) - [@brad-jones](https://github.com/brad-jones)
- **(lint)** fix typo in dotnet tool restore cmd - ([b52b18e](https://github.com/brad-jones/dotnetup/commit/b52b18e806f5043eccffaa36e3ecb2feaf1b4c42)) - [@brad-jones](https://github.com/brad-jones)
- **(lint)** the whole devcontainer thing in ci still need work IMO - ([6c1d233](https://github.com/brad-jones/dotnetup/commit/6c1d2335ea738f10aed7e0903ca0d28323ffd289)) - [@brad-jones](https://github.com/brad-jones)
- **(lint)** try and use the container directly - ([8e1d3cf](https://github.com/brad-jones/dotnetup/commit/8e1d3cf2743b0b078214182cf8953a2eb8e02c4c)) - [@brad-jones](https://github.com/brad-jones)
- **(lint)** authenticate docker so we can push the img - ([7c60be8](https://github.com/brad-jones/dotnetup/commit/7c60be8804cb0beda2a6f6fc769665d5555cba9c)) - [@brad-jones](https://github.com/brad-jones)
- **(lint)** forgot to checkout repo - ([21276f4](https://github.com/brad-jones/dotnetup/commit/21276f4ed39e7a1fecf05b5db0c50b20a4111999)) - [@brad-jones](https://github.com/brad-jones)
- **(lint)** try out devcontainer ci action - ([b4d7316](https://github.com/brad-jones/dotnetup/commit/b4d73169462d1241e93e968704a89529fbcf5c65)) - [@brad-jones](https://github.com/brad-jones)
- **(lint)** debug why dprint is still not executeable - ([252a63c](https://github.com/brad-jones/dotnetup/commit/252a63c196e3d96cb6ca5a5025f461134eec4148)) - [@brad-jones](https://github.com/brad-jones)
- **(lint)** aded dprint to path - ([1e4cdb7](https://github.com/brad-jones/dotnetup/commit/1e4cdb7bccad0a5d9a03be5acd70a4b41b5f93e5)) - [@brad-jones](https://github.com/brad-jones)
- **(publish)** download into the bin folder - ([2053b8e](https://github.com/brad-jones/dotnetup/commit/2053b8e7549dc140596de7537795ae332820cf20)) - [@brad-jones](https://github.com/brad-jones)
- **(publish)** lets see what output download-artifact gives us - ([351b64f](https://github.com/brad-jones/dotnetup/commit/351b64f5f17dee8bd4a35af2056ceab58549ae23)) - [@brad-jones](https://github.com/brad-jones)
- change retention period to minimum - ([8e64abe](https://github.com/brad-jones/dotnetup/commit/8e64abe5568a985771d2a290ab3c8b1eebda5c75)) - [@brad-jones](https://github.com/brad-jones)
- reintroduce the publish step - ([4ae37e7](https://github.com/brad-jones/dotnetup/commit/4ae37e7e92e804477d25e74f7b2fd5369a9367bb)) - [@brad-jones](https://github.com/brad-jones)
- fix for cross-device link error - ([f81b0ec](https://github.com/brad-jones/dotnetup/commit/f81b0ecf68eeb33cd845087966a5a00945df0160)) - [@brad-jones](https://github.com/brad-jones)
- checkout full repo so we can calc next version bump - ([c6799f9](https://github.com/brad-jones/dotnetup/commit/c6799f921c7014fe36cb372dcfcdb6328702f79d)) - [@brad-jones](https://github.com/brad-jones)
- fix shfmt install on windows - ([d6b504c](https://github.com/brad-jones/dotnetup/commit/d6b504c4c00cec491c1760dfbaf1a4e3f62f3197)) - [@brad-jones](https://github.com/brad-jones)
- reintroduce the build_test step - ([98837d1](https://github.com/brad-jones/dotnetup/commit/98837d1fabd3c619ba3a8f078806cabef0d7a2b4)) - [@brad-jones](https://github.com/brad-jones)
- fix path to local action - ([20b9e0a](https://github.com/brad-jones/dotnetup/commit/20b9e0a01133bb2e7b11101f0ffe8d1ca2e9f5bc)) - [@brad-jones](https://github.com/brad-jones)
- lets try this pattern - ([7ff5fb3](https://github.com/brad-jones/dotnetup/commit/7ff5fb32c5762bc64e4e66222879e6e95052f014)) - [@brad-jones](https://github.com/brad-jones)
- opps got the subdir incorrect I think - ([7e0eda2](https://github.com/brad-jones/dotnetup/commit/7e0eda21e9b6611baddc714ee56c2112d1dba0b1)) - [@brad-jones](https://github.com/brad-jones)
- cog is again in a sub dir - ([8877c67](https://github.com/brad-jones/dotnetup/commit/8877c672eb6cf434a7d81501bd327193e4d81c37)) - [@brad-jones](https://github.com/brad-jones)
- shfmt is a pure binary - ([aa1b28a](https://github.com/brad-jones/dotnetup/commit/aa1b28a65cf7f2b434424b89e3d65da1a2e7c5c3)) - [@brad-jones](https://github.com/brad-jones)
- try all the other tools now - ([2e18ead](https://github.com/brad-jones/dotnetup/commit/2e18ead8d61f91347716a0cce9308af34282ee94)) - [@brad-jones](https://github.com/brad-jones)
- test new gh release installer action - ([b8c69e4](https://github.com/brad-jones/dotnetup/commit/b8c69e47dcb9ed1e33f3ee0b1f8acba3a32f3080)) - [@brad-jones](https://github.com/brad-jones)
- cog is extracted into a sub dir unlike the others - ([4fb3152](https://github.com/brad-jones/dotnetup/commit/4fb315284e229e1bdfb88b66392c77955f62f746)) - [@brad-jones](https://github.com/brad-jones)
- debug new instalation method - ([1958e7c](https://github.com/brad-jones/dotnetup/commit/1958e7caab529a6a827141796abcea604a05e05b)) - [@brad-jones](https://github.com/brad-jones)
- use the release-downloader for everything - ([9fd4850](https://github.com/brad-jones/dotnetup/commit/9fd485034b5bf1cd995a14cfbef2986126478c76)) - [@brad-jones](https://github.com/brad-jones)
#### Features
- initial release - ([749bb59](https://github.com/brad-jones/dotnetup/commit/749bb596dc6d48ad503500b6e8ef6928cb4bf52e)) - [@brad-jones](https://github.com/brad-jones)
#### Miscellaneous Chores
- **(devcontainer)** add gitlens - ([96c3a4e](https://github.com/brad-jones/dotnetup/commit/96c3a4e6f59f83c1716e2fa8e5f7918db056842a)) - [@brad-jones](https://github.com/brad-jones)
- **(gitignore)** ignore the publish dir we create at publish time - ([6a17d2b](https://github.com/brad-jones/dotnetup/commit/6a17d2bd15bf2d7c147660864da40b0fa58b7bde)) - [@brad-jones](https://github.com/brad-jones)

- - -

Changelog generated by [cocogitto](https://github.com/cocogitto/cocogitto).