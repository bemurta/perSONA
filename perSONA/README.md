## VA (Virtual Acoustics)

VA is the acronym for Virtual Acoustics, the generation of sound based on computation, simulation and reproduction.
The main goal is to support those who want to create and design audio-visual Virtual Reality.
VA provides acoustics applications with real-time capability that can be controlled by a network interface.

This project includes *C++ libraries*, *bindings* to other programming and scripting languages as well as *applications* for real-time auralization.

### History

The concept of VA was invented and developed in a series of scientific projects at the Institute of Technical Acoustics (ITA), RWTH Aachen University. It was first introduced as a software application for loudspeaker-based real-time auralization using binaural rendering and 4-channel cross-talk cancellation by Tobias Lentz. It made progress with a joint project for an audio-visual application in the first CAVE-like system in close collaboration with the [Virtual Reality Group led by Prof. Torsten Kuhlen at the RWTH Aachen University](http://www.vr.rwth-aachen.de).

After the decision to build a [new CAVE-like system](http://www.itc.rwth-aachen.de/cms/IT-Center/Forschung-Projekte/Virtuelle-Realitaet/Infrastruktur/~fgqa/aixCAVE/), Frank Wefers started to re-invent Virtual Acoustics under the acronym VA and added support for a sophisticated C++ interfacing that also allows for networked bindings to be implemented fairly easy.


### Purpose and license

VA has been made available to the public in the spirit of sharing knowledge, giving possibility to build upon it and employing it for scientific research that should be reproducible. In practice this usually requires a binary version of specialized VA package to be uploaded to an open access research repository for a certain amount of time (like 10 years).

The final license of a VA binary package depends on the *build configuration* and hence a *license compatibility with depending packages* that are linked against the VACore (in case of dynamic linking) and/or the final applications using VA (like Redstart and VAServer). Therefore, we have decided to generally use the permissive Apache License, Version 2.0, for our entire code base. It releases any researcher from the burden of imposing a dedicated license, yet requires stating the name and making clear where the original source of the version used can be obtained from - very much like a thorough (data) citation in a scientific publication.

Unfortunately, any copyleft license, notably the GNU General Public License (GPL) and the GNU Lesser GPL, is incompatible using it this way (distributing Apache licensed binary packages that link against GPL libraries). VA is not per se using GPL dependencies (hence we do not per se use (L)GPL for our code) but there are build configurations that demand it. As a solution, we license those VA binary distributions (linking against (L)GPL libraries) under the respective license, subjogating to the viral nature of copyleft. This approach has similarity to dual licensing Apache and newer GPL, but we do not give a *choice* as the Apache license should be favored when no copyleft dependency is included, as it is compatible with newer GPL licenses anyway (in other words: *can be linked against by a GPL binary*). We clearly state that we support the idea of open source software and want to act in that spirit by encouraging further open source usage. However, we do not want to enforce a specific copyleft license to be used in open science practice, as this might exclude scientific institutions that can not follow this path.

#### VA code license

Copyright 2015-2018 Institute of Technical Acoustics (ITA), RWTH Aachen University.

Licensed under the Apache License, Version 2.0 (the "License");
you may not use files of this project except in compliance with the License.
You may obtain a copy of the License at

<http://www.apache.org/licenses/LICENSE-2.0>

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.

#### VA binaries license (selective license depending on third party library licenses)

If a distributed binary package of VA is provided for download and (partly) links against GPL or LGPL libraries, the resulting binaries will be published under the GPL or LGPL license. This occurs when a certain combination of dependencies created by the VA build configuration pulls in copyleft libraries.

If VA build configuration has made it necessary, find the corresponding (L)GPL license file in the root folder of a VA package, usually named `COPYING` and/or `COPYING.LESSER`. The Apache license formulating the license for the VA code will always be named LICENSE.md, and only refers to the source files and libraries that are _not linking against an (L)GPL library_.

> A warning note: if you have received a VA package and are linking against the VACore library, be aware that you have to make your work compatible with (L)GPL, if it is necessary (if there is a LICENSE.(L)GPL.md file).
> If you are redistributing a VA package, do not remove any license file.
> If you are insecure, there is no harm in licensing your source and binary under (L)GPL if you are OK sharing your code and you don't believe that anyone will be building upon your work (and imposing to continue using GPL along the dependency chain).



### Logo

The VA logo is a figurative trade mark and has a dedicated license. You may use the logo under certain conditions. For more information, see the [README file in the logo folder](logo/README.md).

<img src="https://git.rwth-aachen.de/ita/VA/raw/e97d635c02a73653eae688ca4cd3dc8a2523dea1/logo/VA_logo_black_circle_white.png" width="250px" alt="Official solitaire VA (Virtual Acoustics) logo, black & transparent" />


### Quick build guide

VA is a collective of repositories. Clone VA using the `--recursive` flag, since it mostly consists of submodules.
Use CMake and the top-level CMakeLists.txt of the VA project. It will generate a poject file with alle sub-projects included.
VA makes heavy use of [ViSTA, the Virtual Reality Toolkit](https://sourceforge.net/projects/vistavrtoolkit/files/) developed by the [Virtual Reality Group of the IT Center, RWTH Aachen University](http://www.itc.rwth-aachen.de/cms/IT-Center/Forschung-Projekte/~eubl/Virtuelle-Realitaet/).
Hence, the build environment requires the VistaCMakeCommon extension for CMake in order to define and resolve all required dependencies.
For more information, see the [Wiki pages of ITACoreLibs](https://git.rwth-aachen.de/ita/ITACoreLibs/wikis/home).


#### Working with submodules

If you do not want to make any changes but update to the latest HEAD revision, use the command `git submodule update --remote` in order to also update the submodules.
If you have already made changes, updating will fail.

If you want to make changes to the submodule project, browse into the directory and checkout a branch since a submodule is per default detached from HEAD, i.e. the master branch:

`git checkout master`

Now make your changes, add, commit and push from this location as usual.

For convenience, GIT also provides a batch command that performs the call for each submodule:
`git submodule foreach git checkout master`

For switching branches while skipping if a specific branch is not present, use
`git submodule foreach "git checkout develop || true"`


#### Switching to a specific version

To use a spefic version of a VA submodule, say VACore, you have to `cd VACore` and `git checkout v2017.a`. From now on, take care when updating.


#### Cleaning up

Sometimes, submodules remain in a dirty state because files are created by the build environment - which are not under version control.
If you want to clean up your working directory, use `git submodule foreach git clean -f -n`, and remove the `-n` if your are sure to remove the listed files in the submodules.
Afterwards, a `git submodule status` should return a clean copy.:
