# RE4-UHD-PACKYZ2-TOOL
Extract and repack RE4 UHD .pack/.pack.yz2 files

Translate from Portuguese Brazil

Programa destinado a extrair e reempacotar arquivos .pack/pack.yz2
<br> Ao extrair será gerado um arquivo de extenção .idxpack, ele será usado para o repack.

## Extract

Exemplo:
<br>JADERLINK_PACKYZ2_TOOL.exe "01000000.pack.yz2"

* Ira gerar um arquivo de nome "01000000.pack.yz2.idxpack"
* Ira criar uma pasta de nome "01000000"
* Na pasta vai conter as texturas, nomadas numericamente com 4 digitos. Ex: 0000.dds

## Repack

Exemplo:
<br>JADERLINK_PACKYZ2_TOOL.exe "01000000.pack.yz2.idxpack"

* vai ler as imagem da pasta "01000000"
* a quantidade é definido pela numeração, (então não deixe imagens faltando no meio).
* o nome do arquivo gerado, é o mesmo nome do idxpack, mas sem o .idxpack;

**At.te: JADERLINK**
<br>2023-10-14