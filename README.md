# RE4-UHD-PACKYZ2-TOOL
Extract and repack RE4 UHD .pack/.pack.yz2 files

**Translate from Portuguese Brazil**

Programa destinado a extrair e reempacotar arquivos .pack/pack.yz2 da versão de PC UHD;
<br> Ao extrair será gerado um arquivo de extenção .idxpack, ele será usado para o repack.

**update: 1.0.7**
<br>Corrigido bug ao extrair arquivos de imagens com 0 de tamanho.
<br>Agora, ao arrastar arquivos sobre o programa, ele vai ficar aberto após extrair/reempacotar.
<br>Os arquivos bat funcionam iguais a antes, mas agora adicionei mais um parâmetro neles.

**update: 1.0.6**
<br>Agora, a tool aceita vários arquivos como parâmetro, assim, podendo extrair ou recompactar vários arquivos .pack;

**update: 1.0.4**
<br> Melhorias no código.

**update: 1.0.3**
<br>Adicionado suporte ao arquivo de formato ".reference", dentro dele vai ter o ID de uma textura anterior referenciada, serve para colocar a mesma textura em mais de um ID, ocupando o espaço em disco de um único arquivo de textura, porém na memória do jogo, vai ocupar o espaço de duas texturas.

**update: 1.0.2**
<br>Arrumado o alinhamento dos arquivos, corrigidos bugs.

**update: 1.0.1**
<br>Agora, além dos arquivos .dds e .tga, ele aceita arquivos .empty que representa que aquela numeração está vazia, assim pode você pular a numeração sem ocupar mais espaço no arquivo.
<br>Nota: você não pode fazer referência a numerações de arquivos "empty" no tpl, pois realmente não existe imagem ali. No lugar, será exibida a textura de botões.

## Extract

Exemplo:
<br>*RE4_UHD_PACKYZ2_TOOL.exe "01000000.pack.yz2"*

* Vai gerar um arquivo de nome "01000000.pack.yz2.idxpack";
* Vai criar uma pasta de nome "01000000";
* Na pasta vão conter as texturas, nomeadas numericamente com 4 dígitos. Ex: 0000.dds;

## Repack

Exemplo:
<br>*RE4_UHD_PACKYZ2_TOOL.exe "01000000.pack.yz2.idxpack"*

* Vai ler as imagens da pasta "01000000";
* A quantidade é definida pela numeração (então não deixe imagens faltando no meio);
* O nome do arquivo gerado é o mesmo nome do idxpack, mas sem o .idxpack;

**At.te: JADERLINK**
<br>2025-01-07